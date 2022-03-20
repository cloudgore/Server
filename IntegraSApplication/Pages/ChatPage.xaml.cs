using IntegraSApplication.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IntegraSApplication.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {

        private delegate void ChatEvent(string content, string clr);
        private ChatEvent _addMessage;
        private Socket _serverSocket;
        private Thread listenThread;
        private string _host = ConfigurationManager.AppSettings.Get("IP");
        private int _port = int.Parse(ConfigurationManager.AppSettings.Get("port"));


        public ChatPage()
        {
            InitializeComponent();
            DataContext = User.userAunt;
            SendMsg.IsEnabled = false;
            MsgTb.IsEnabled = false;
            _addMessage = new ChatEvent(AddMessage);

        }
        private void Connect()
        {
            try
            {
                IPAddress temp = IPAddress.Parse(_host);
                _serverSocket = new Socket(temp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _serverSocket.Connect(new IPEndPoint(temp, _port));

                string nickName = User.userAunt.UserName;
                if (string.IsNullOrEmpty(nickName))
                    return;
                Send($"#setname|{nickName}");
                if (_serverSocket.Connected)
                {
                    AddMessage("Связь с сервером установлена.");
                    listenThread = new Thread(listner);
                    listenThread.IsBackground = true;
                    listenThread.Start();
                }
                else
                    AddMessage("Связь с сервером не установлена.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void AddMessage(string Content, string Color = "Black")
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(_addMessage, Content, Color);
                return;
            }
            listChat.Items.Add(Content);

        }

        public void Send(byte[] buffer)
        {
            try
            {
                _serverSocket.Send(buffer);
            }
            catch { }
        }
        public void Send(string Buffer)
        {
            try
            {
               _serverSocket.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }
        public void handleCommand(string cmd)
        {

            string[] commands = cmd.Split('#');
            int countCommands = commands.Length;
            for (int i = 0; i < countCommands; i++)
            {
                try
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (currentCommand.Contains("setnamesuccess"))
                    {
                        AddMessage($"Добро пожаловать, {User.userAunt.UserName}");
                        continue;
                    }
                    if (currentCommand.Contains("msg"))
                    {
                        string[] Arguments = currentCommand.Split('|');

                        int k = Arguments[1].IndexOf("Печатает");
                        if(k != -1)
                        {
                            string substr = Arguments[1].Substring(k);
                            if (substr.Contains("Печатает"))
                                AddMessage(substr);
                        }
                        else
                            AddMessage(Arguments[1], Arguments[2]);
                        continue;
                    }
                    if (currentCommand.Contains("remove"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        int k = listChat.Items.IndexOf(Arguments[1]);
                        Dispatcher.Invoke(() => listChat.Items.RemoveAt(k));
                    }
                }
                catch (Exception exp) { Console.WriteLine("Error with handleCommand: " + exp.Message); }
            }
        }

        private void PrivateMsg(string privateMsg)
        {
            string[] SplitMsg = privateMsg.Split('|');
            if(privateMsg !=null)
            Send($"#private|{SplitMsg[0]}|{SplitMsg[1]}");
        }

        public void listner()
        {
            try
            {
                while (_serverSocket.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = _serverSocket.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch
            {
                MessageBox.Show("Связь с сервером прервана");
            }
        }
        private void SendMsgClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (listChat.Items.Contains("Печатает"))
                {
                    Send($"#rmv|Печатает");
                    string msgData = MsgTb.Text;
                    if (string.IsNullOrEmpty(msgData))
                        return;
                    Send($"#message|{msgData}");
                    MsgTb.Text = string.Empty;
                }
                else
                {
                    string msgData = MsgTb.Text;
                    if (string.IsNullOrEmpty(msgData))
                        return;
                    Send($"#message|{msgData}");
                    MsgTb.Text = string.Empty;
                }

            }
            catch
            {
                MessageBox.Show("Ошибка при отправки сообщения");
            }
        }

        private void ConnectClick(object sender, RoutedEventArgs e)
        {
            if (ConnectBtn.Content.Equals("Подключиться"))
            {
                Connect();
                ConnectBtn.Content = "Отключиться";
                Send("#endsession");
                SendMsg.IsEnabled = true;
                MsgTb.IsEnabled = true;
            }
            else
            {
                ConnectBtn.Content = "Подключиться";
                SendMsg.IsEnabled = false;
            }


        }


        private void MouseClick(object sender, TextChangedEventArgs e)
        {
            if (!listChat.Items.Contains("Печатает"))
            {
                try
                {
                    string msgData = MsgTb.Text;
                    if (string.IsNullOrEmpty(msgData))
                        return;
                    Send($"#message|Печатает");
                }
                catch
                {
                    MessageBox.Show("Ошибка при отправки сообщения");
                }
            }
            else
                return;
        }

        private void EnteClick(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                try
                {
                    if (listChat.Items.Contains("Печатает"))
                    {
                        Send($"#rmv|Печатает");
                        string msgData = MsgTb.Text;
                        if (string.IsNullOrEmpty(msgData))
                            return;
                        Send($"#message|{msgData}");
                        MsgTb.Text = string.Empty;
                    }
                    else
                    {
                        string msgData = MsgTb.Text;
                        if (string.IsNullOrEmpty(msgData))
                            return;
                        Send($"#message|{msgData}");
                        MsgTb.Text = string.Empty;
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка при отправки сообщения");
                }
            }
          
        }

        private void DellMsgClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить сообщение?") == MessageBoxResult.OK)
                {
                    Send($"#rmv|{listChat.SelectedItem}");
                }

            }
            catch
            {
                MessageBox.Show("Ошиюка");
            }
        }

        private void PrivateMsgClick(object sender, RoutedEventArgs e)
        {
            PrivateMsg(MsgTb.Text);
        }
    }

}