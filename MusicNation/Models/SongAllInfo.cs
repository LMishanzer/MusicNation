using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicNation.Models
{
    public class SongAllInfo
    {
        public string Title { get; set; }
        public string AlbumTitle { get; set; }
        public string Artist { get; set; }
        public int? Year { get; set; }
        public string IdOnDrive { get; set; }
    }
}
