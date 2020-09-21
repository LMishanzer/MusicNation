using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MusicNation.Models.Database
{
    public class MusicContext : DbContext
    {
        public MusicContext() { }

        public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
    }
}
