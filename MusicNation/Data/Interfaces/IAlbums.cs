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

        Task<bool> AddAlbum(Album album);
    }
}
