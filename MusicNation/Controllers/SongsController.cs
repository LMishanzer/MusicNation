using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicNation.Data.Interfaces;
using MusicNation.Models.GoogleDrive;

namespace MusicNation.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongs songs;
        private readonly DriveSession session;

        public SongsController(ISongs songs)
        {
            this.songs = songs;
            session = new DriveSession();
        }

        public ViewResult List()
        {
            var songs = this.songs.GetAllSongs();
            return View(songs);
        }

        public async Task<IActionResult> Download(string button)
        {
            var stream = await session.Download(button);

            var contentType = "audio/mpeg";
            var fileName = $"{button}.mp3";

            var file = File(stream, contentType, fileName);

            return file;
        }
    }
}
