using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MusicNation.Models
{
    public class CreatePost
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public int Year { get; set; }
        public IFormFile Song { get; set; }
    }
}
