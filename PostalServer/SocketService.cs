using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PostalServer
{
    static class SocketService
    {

        private static int port = 5001;

        private static IPAddress ipAddress = IPAddress.Parse("127.0.0.1");

        private static IPEndPoint iPEndPoint = new IPEndPoint(ipAddress, port);

        private static PostalInfo infoHolder = new PostalInfo();

        private static string notFoundMessage = "Addresses by this index not found";

  

        public static void Start()
        {
            Socket handler = null;
            try
            {
                Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listenSocket.Bind(iPEndPoint);

                listenSocket.Listen(10);

                while (true)
                {
                    handler = listenSocket.Accept();
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; 
                    byte[] data = new byte[1024]; 

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    string message = Encoding.UTF8.GetString(data);
                    infoHolder = DbService.GetPostalInfoByIndex(message);
                    if(infoHolder != null)
                    {
                        message = JsonConvert.SerializeObject(infoHolder);
                        handler.Send(Encoding.UTF8.GetBytes(message));
                    }
                    else
                    {
                        handler.Send(Encoding.UTF8.GetBytes(notFoundMessage));
                    }
                    
                    
                }
            } catch (SocketException exception)
            {
                Console.WriteLine(exception.Message);
            
            }
        }
    }
}
