using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class User
    {
        private Thread _userThread;
        private string _userName;
        private bool AuthSuccess = false;
        public string Username
        {
            get { return _userName; }
        }
        private Socket _userHandle;
        public User(Socket handle)
        {
            _userHandle = handle;
            _userThread = new Thread(listner);
            _userThread.IsBackground = true;
            _userThread.Start();
        }
        private void listner()
        {
            try
            {
                while (_userHandle.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = _userHandle.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch { Server.EndUser(this); }
        }
        private bool setName(string Name)
        {
            //Тут можно добавить различные проверки
            _userName = Name;
            Server.NewUser(this);
            AuthSuccess = true;
            return true;
        }
        private void handleCommand(string cmd)
        {
            try
            {
                string[] commands = cmd.Split('#');
                int countCommands = commands.Length;
                for (int i = 0; i < countCommands; i++)
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (!AuthSuccess)
                    {
                        if (currentCommand.Contains("setname"))
                        {
                            if (setName(currentCommand.Split('|')[1]))
                                Send("#setnamesuccess");
                            else
                                Send("#setnamefailed");
                        }
                        continue;
                    }
                    // Сообщение
                    if (currentCommand.Contains("message"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.SendGlobalMessage($"[{_userName}]: {Arguments[1]}","Black");
                        continue;
                    }
                    // Отключение
                    if(currentCommand.Contains("endsession"))
                    {
                        Server.EndUser(this);
                        return;
                    }
                    // Удалить сообщение у всех пользователей 
                    if (currentCommand.Contains("rmv"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        Server.RemoveGlobalMessage(Arguments[1]);
                    }
                   // Приватное сообщение
                     if(currentCommand.Contains("private"))
                     {
                         string[] Arguments = currentCommand.Split('|');
                         string TargetName = Arguments[1];
                         string Content = Arguments[2];
                         User targetUser = Server.GetUser(TargetName);
                         if(targetUser == null)
                         {
                             SendMessage($"Пользователь {TargetName} не найден!","Red");
                             continue;
                         }
                         SendMessage($"-[Отправлено][{TargetName}]: {Content}","Black");
                         targetUser.SendMessage($"-[Получено][{Username}]: {Content}","Black");
                         continue;
                     }
                    
                    
                }

            }
            catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
        }
        
        public void RemoveMassage(string content)
        {
            Send($"#remove|{content}");
        }

        public void SendMessage(string content,string clr)
        {
            Send($"#msg|{content}|{clr}");
        }
        public void Send(byte[] buffer)
        {
            try
            {
                _userHandle.Send(buffer);
            }
            catch (SocketException exception)
            {
                Console.WriteLine(exception.ErrorCode);
            }
        }
        public void Send(string Buffer)
        {
            try
            {
                _userHandle.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch (SocketException exception)
            {
                Console.WriteLine(exception.ErrorCode);
            }
        }
        public void End()
        {
            try
            {
                _userHandle.Close();
            }
            catch { }

        }
    }
}
