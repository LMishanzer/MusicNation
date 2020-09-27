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
        private readonly MusicContext dbContext;

        public MockArtists(MusicContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Artist> GetAllArtists() => dbContext.Artists;

        public async Task<bool> AddArtist(Artist artist)
        {
            try
            {
                await dbContext.Artists.AddAsync(artist);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
