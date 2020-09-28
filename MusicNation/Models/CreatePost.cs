using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MusicNation.Models
{
    public class CreatePost
    {
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Title { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Artist { get; set; }
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Album { get; set; }
        [Range(1900, 2020)]
        public int Year { get; set; }
        [Required]
        public IFormFile Song { get; set; }
    }
}
