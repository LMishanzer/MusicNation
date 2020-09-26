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
    public class UploadController : Controller
    {
        private readonly ISongs songs;
        private readonly DriveSession session;

        public UploadController(ISongs songs)
        {
            this.songs = songs;
            session = new DriveSession();
        }


        public IActionResult Index()
        {
            var post = new CreatePost();
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(CreatePost post)
        {
            await using (var stream = new MemoryStream())
            {
                await post.Song.CopyToAsync(stream);

                if (stream.Length < 20971520)
                {
                    await session.Upload(stream, post.Name);
                }
                else
                {
                    ModelState.AddModelError("File.", "The file is too large");
                }
            }

            return View("Index");
        }
    }
}
