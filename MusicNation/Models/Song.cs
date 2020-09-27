using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicNation.Models
{
    public class Song
    {
        private static int _counter;

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string IdOnDrive { get; set; }

        public Song() { }

        public Song(string title, Album album, string idOnDrive)
        {
            Title = title;
            Album = album;
            AlbumId = album.Id;
            IdOnDrive = idOnDrive;
            Id = _counter++;
        }
    }
}
