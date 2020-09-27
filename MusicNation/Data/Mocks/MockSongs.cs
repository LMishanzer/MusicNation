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
        private readonly MusicContext _dbContext;

        public MockSongs(MusicContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<SongAllInfo> GetAllSongs()
        {
            var songs = _dbContext.Songs;
            var albums = _dbContext.Albums;
            var artists = _dbContext.Artists;

            var wholeSongs = songs
                .Join(
                    albums,
                    song => song.AlbumId,
                    album => album.Id,
                    (song, album) => new
                    {
                        SongId = song.Id,
                        AlbumId = album.Id,
                        album.ArtistId,
                        SongTitle = song.Title,
                        AlbumTitle = album.Title,
                        album.Year,
                        song.IdOnDrive
                    }
                )
                .Join(
                    artists,
                    song => song.ArtistId,
                    artist => artist.Id,
                    (song, artist) => new SongAllInfo()
                    {
                        Title = song.SongTitle,
                        AlbumTitle = song.AlbumTitle,
                        Artist = artist.Name,
                        Year = song.Year,
                        IdOnDrive = song.IdOnDrive
                    }
                );

            return wholeSongs;
        }

        public async Task<bool> AddSong(Song song)
        {
            try
            {
                await _dbContext.Songs.AddAsync(song);
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
