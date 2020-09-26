using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicNation.Models
{
    public class Song
    {
        private static int counter = 0;

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string IdOnDrive { get; set; }

        public Song() { }

        public Song(string name, Album album)
        {
            Name = name;
            Album = album;
            AlbumId = album.Id;
            Id = counter++;
        }
    }
}
