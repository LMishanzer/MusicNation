using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicNation.Data.Interfaces;
using MusicNation.Models;
using MusicNation.Models.Database;

namespace MusicNation.Data.Mocks
{
    public class MockArtists : IArtists
    {
        private readonly MusicContext _dbContext;

        public MockArtists(MusicContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Artist> GetAllArtists() => _dbContext.Artists;

        public Artist GetArtist(int id) => _dbContext.Artists.Single(artist => artist.Id == id);

        public async Task<bool> AddArtist(Artist artist)
        {
            try
            {
                await _dbContext.Artists.AddAsync(artist);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
