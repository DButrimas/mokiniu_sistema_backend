using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mokiniu_sistema.Models
{
    public class MokiniuContext : DbContext
    {
        public DbSet<Mokykla> Mokyklos { get; set; }
        public DbSet<Rajonas> Rajonai { get; set; }
        public DbSet<Tevas> Tevai { get; set; }
        public DbSet<Vaikas> Vaikai { get; set; }
        public MokiniuContext(DbContextOptions<MokiniuContext> options)
            : base(options)
        {

        }
    
    }
}
