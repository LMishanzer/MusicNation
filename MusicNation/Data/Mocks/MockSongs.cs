﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicNation.Data.Interfaces;
using MusicNation.Models;
using MusicNation.Models.Database;

namespace MusicNation.Data.Mocks
{
    public class MockSongs : ISongs
    {
        private readonly MusicContext dbContext;

        public MockSongs(MusicContext context)
        {
            dbContext = context;
        }

        public IEnumerable<Song> GetAllSongs() => dbContext.Songs;

        public async Task<bool> AddSong(Song song)
        {
            try
            {
                await dbContext.Songs.AddAsync(song);
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
