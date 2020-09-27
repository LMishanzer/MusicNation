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
        public async Task<IActionResult> Upload(CreatePost post)
        {
            await using (var stream = new MemoryStream())
            {
                await post.Song.CopyToAsync(stream);

                if (stream.Length < 20971520)
                {
                    await _session.Upload(stream, post.Name);

                    var artist = new Artist(post.Artist);
                    var album = new Album(post.Album, post.Year, artist);
                    var song = new Song(post.Name, album, _session.GetFileId(post.Name));

                    await _artists.AddArtist(artist);
                    await _albums.AddAlbum(album);
                    await _songs.AddSong(song);
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
