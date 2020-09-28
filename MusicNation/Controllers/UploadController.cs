using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicNation.Data.Interfaces;
using MusicNation.Models;
using MusicNation.Models.GoogleDrive;

namespace MusicNation.Controllers
{
    public class UploadController : Controller
    {
        private readonly ISongs _songs;
        private readonly IAlbums _albums;
        private readonly IArtists _artists;
        private readonly DriveSession _session;

        public UploadController(ISongs songs, IAlbums albums, IArtists artists)
        {
            _songs = songs;
            _albums = albums;
            _artists = artists;
            _session = new DriveSession();
        }

        public IActionResult Index()
        {
            var post = new CreatePost();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(CreatePost post)
        {
            if (post.Song?.Length > 20971520)
            {
                ModelState.AddModelError("Song", "The file is too large");
            }

            if (post.Song?.ContentType != "audio/mpeg")
            {
                ModelState.AddModelError("Song", "The file is not song");
            }

            if (ModelState.IsValid)
            {
                await using var stream = new MemoryStream();
                await post.Song.CopyToAsync(stream);

                await _session.Upload(stream, post.Title);

                var artist = new Artist(post.Artist);
                var album = new Album(post.Album, post.Year, artist);
                var song = new Song(post.Title, album, _session.GetFileId(post.Title));

                await _artists.AddArtist(artist);
                await _albums.AddAlbum(album);
                await _songs.AddSong(song);
            }

            return View("Index");
        }
    }
}
