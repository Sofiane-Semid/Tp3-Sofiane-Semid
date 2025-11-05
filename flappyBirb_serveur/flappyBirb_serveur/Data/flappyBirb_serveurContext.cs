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

        public DbSet<flappyBirb_serveur.Models.Score> Score { get; set; } = default!;
    }
}
