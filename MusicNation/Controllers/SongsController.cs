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
        private readonly IAlbums _albums;
        private readonly IArtists _artists;
        private readonly DriveSession _session;

        public SongsController(ISongs songs, IAlbums albums, IArtists artists)
        {
            _songs = songs;
            _albums = albums;
            _artists = artists;
            _session = new DriveSession();
        }

        public ViewResult Artists()
        {
            var artists = _artists.GetAllArtists();

            return View(artists);
        }

        public ViewResult Albums(int artistId)
        {
            var albums = _albums.GetAlbumsByArtistId(artistId);
            ViewData["artist"] = _artists.GetArtist(artistId).Name;

            return View(albums);
        }

        public ViewResult Songs(int albumId)
        {
            var songs = _songs.GetSongsByAlbum(albumId);
            var album = _albums.GetAlbum(albumId);
            var artist = _artists.GetArtist(album.ArtistId);

            ViewData["album"] = album.Title;
            ViewData["artist"] = artist.Name;

            return View(songs);
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
