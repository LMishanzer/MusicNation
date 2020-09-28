using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicNation.Models;

namespace MusicNation.Data.Interfaces
{
    public interface IAlbums
    {
        IEnumerable<Album> GetAllAlbums();

        IEnumerable<Album> GetAlbumsByArtistId(int artistId);

        Album GetAlbum(int id);

        Task<bool> AddAlbum(Album album);
    }
}
