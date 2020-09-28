using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicNation.Models;

namespace MusicNation.Data.Interfaces
{
    public interface ISongs
    {
        IEnumerable<SongAllInfo> GetAllSongs();

        IEnumerable<Song> GetSongsByAlbum(int albumId);

        IEnumerable<Song> GetSongsByArtist(int artistId);

        Task<bool> AddSong(Song song);
    }
}
