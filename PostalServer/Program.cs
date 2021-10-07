using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalServer
{
    class Program
    {
        static void Main(string[] args)
        {
            DbService.Start();
            SocketService.Start();

           
        }
    }
}
