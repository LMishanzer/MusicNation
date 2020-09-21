using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbFiller2.Models
{
    public class Album
    {
        private static int counter = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }

        public Album() { }

        public Album(string name, int year, Artist artist)
        {
            Name = name;
            Year = year;
            Artist = artist;
            ArtistId = artist.Id;
            Id = counter++;
        }
    }
}
