using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbFiller2.Models
{
    public class Artist
    {
        private static int counter = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }

        public Artist() { }

        public Artist(string name)
        {
            Name = name;
            Id = counter++;
        }
    }
}
