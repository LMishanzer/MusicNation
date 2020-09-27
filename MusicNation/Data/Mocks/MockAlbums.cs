using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicNation.Data.Interfaces;
using MusicNation.Models;
using MusicNation.Models.Database;

namespace MusicNation.Data.Mocks
{
    public class MockAlbums : IAlbums
    {
        private readonly MusicContext dbContext;

        public MockAlbums(MusicContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Album> GetAllAlbums() => dbContext.Albums;
        public async Task<bool> AddAlbum(Album album)
        {
            try
            {
                await dbContext.Albums.AddAsync(album);
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
