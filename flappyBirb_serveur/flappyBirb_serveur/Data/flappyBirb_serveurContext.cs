using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using flappyBirb_serveur.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace flappyBirb_serveur.Data
{
    public class flappyBirb_serveurContext : IdentityDbContext<User>
    {
        public flappyBirb_serveurContext (DbContextOptions<flappyBirb_serveurContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Conservez cette ligne de code en tout temps
        }


        public DbSet<Score> Score { get; set; } = default!;
    }
}
