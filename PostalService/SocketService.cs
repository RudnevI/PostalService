using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PostalService
{
    class SocketService
    {
        private static int port = 5001;

        private static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        private static IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, port);

        public static string GetAddresses(string index)
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(iPEndPoint);
                byte[] data = Encoding.UTF8.GetBytes(index);
                socket.Send(data);
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                return Encoding.UTF8.GetString(data);
            }
            catch(SocketException exception)
            {
                Console.WriteLine(exception.Message);
                return "Network error";

            } 
        }
    }
}
