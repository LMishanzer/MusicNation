using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicNation.Data.Interfaces;
using MusicNation.Models;
using MusicNation.Models.GoogleDrive;

namespace MusicNation.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongs _songs;
        private readonly DriveSession _session;

        public SongsController(ISongs songs, IAlbums albums, IArtists artists)
        {
            _songs = songs;
            _session = new DriveSession();
        }

        public ViewResult List()
        {
            var songs = _songs.GetAllSongs();
            return View(songs);
        }

        public async Task<IActionResult> Download(string button)
        {
            var stream = await _session.Download(button);

            var contentType = "audio/mpeg";
            var fileName = $"{button}.mp3";

            var file = File(stream, contentType, fileName);

            return file;
        }
    }
}
