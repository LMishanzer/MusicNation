using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbFiller2.Models
{
    public class Song
    {
        private static int counter = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Duration { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public Song(){ }

        public Song(string name, Album album)
        {
            Name = name;
            Album = album;
            AlbumId = album.Id;
            Id = counter++;
        }
    }
}
