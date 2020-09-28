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
        private readonly MusicContext _dbContext;

        public MockAlbums(MusicContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<Album> GetAllAlbums() => _dbContext.Albums;

        public IEnumerable<Album> GetAlbumsByArtistId(int artistId) =>
            _dbContext.Albums.Where(album => album.ArtistId == artistId);

        public Album GetAlbum(int id) => _dbContext.Albums.Single(album => album.Id == id);

        public async Task<bool> AddAlbum(Album album)
        {
            await _dbContext.Albums.AddAsync(album);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
