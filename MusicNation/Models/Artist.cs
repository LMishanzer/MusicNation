using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicNation.Models
{
    public class Artist
    {
        private static int _counter;

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Album> Albums { get; set; }

        public Artist() { }

        public Artist(string name)
        {
            Name = name;
            Id = _counter++;
        }
    }
}
