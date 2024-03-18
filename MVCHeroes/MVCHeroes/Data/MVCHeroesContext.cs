using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCHeroes.Models;

namespace MVCHeroes.Data
{
    public class MVCHeroesContext : DbContext
    {
        public MVCHeroesContext (DbContextOptions<MVCHeroesContext> options)
            : base(options)
        {
        }

        public DbSet<MVCHeroes.Models.Heroes> Heroes { get; set; } = default!;
    }
}
