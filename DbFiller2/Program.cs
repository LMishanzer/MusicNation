using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFiller2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SongsDbContext())
            {
                Console.WriteLine("Database was connected");

                var filler = new Filler(db);
                filler.Fill();
            }

            Console.WriteLine("Done!");
            Console.Read();
        }
    }
}
