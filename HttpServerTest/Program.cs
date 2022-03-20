using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServerTest
{
    class Program
    {
        static void Main(string[] args)
        {

            HttpListener httpListener = new HttpListener();
            httpListener.Prefixes.Add("http://127.0.0.1:2222/");

            httpListener.Start();

            while (true)
            {
                HttpListenerContext request = httpListener.GetContext();
                var stream = request.Response.OutputStream;
                var text = "test msg";
                var bytes = Encoding.UTF8.GetBytes(text);
                stream.Write(bytes,0,bytes.Length);

                request.Response.StatusCode = 200;
                request.Response.Close();
            }
        }
    }
}
