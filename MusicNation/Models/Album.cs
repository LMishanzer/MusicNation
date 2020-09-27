using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicNation.Models
{
    public class Album
    {
        private static int _counter;

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int? Year { get; set; }
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public ICollection<Song> Songs { get; set; }

        public Album() { }

        public Album(string title, int year, Artist artist)
        {
            Title = title;
            Year = year;
            Artist = artist;
            ArtistId = artist.Id;
            Id = _counter++;
        }
    }
}
