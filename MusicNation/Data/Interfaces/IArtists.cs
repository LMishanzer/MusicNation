using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicNation.Models;

namespace MusicNation.Data.Interfaces
{
    public interface IArtists
    {
        IEnumerable<Artist> GetAllArtists();

        Task<bool> AddArtist(Artist artist);
    }
}
