using Domain;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostalServer
{
    static class DbService
    {
        public static void Start()
        {
            using (var db = new LiteDatabase($@"{Directory.GetCurrentDirectory()}/postal_db.db"))
            {
                var col = db.GetCollection<PostalInfo>("postal_info");

                if (col.Count() == 0)
                {
                    col.Insert(
                        new PostalInfo { Addresses = "Test address 1", Index = "12345" }


                        );
                    col.Insert(
                        new PostalInfo { Addresses = "Test address 2", Index = "666" }

                        );
                }
            }
        }


        public static void DisplayEntities()
        {
            using (var db = new LiteDatabase($@"{Directory.GetCurrentDirectory()}/postal_db.db"))
            {
                var col = db.GetCollection<PostalInfo>("postal_info");
                col.FindAll().ToList().ForEach(element => Console.WriteLine(element.Index));
            }
        }

        public static PostalInfo GetPostalInfoByIndex(string index)
        {
            using (var db = new LiteDatabase($@"{Directory.GetCurrentDirectory()}/postal_db.db"))
            {
                var col = db.GetCollection<PostalInfo>("postal_info");
                return col.FindOne(info => info.Index.Equals(index));
            }
        }
    }
}
