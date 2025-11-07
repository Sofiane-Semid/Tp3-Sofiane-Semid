using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using flappyBirb_serveur.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                                         
            var hasher = new PasswordHasher<User>();

            var u1 = new User
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "sofiane",
                Email = "sofiane@mail.com",
                NormalizedUserName = "sofiane",
                NormalizedEmail = "sofiane@MAIL.COM",
                
            };

            var u2 = new User
            {
                Id = "22222222-2222-2222-2222-222222222222",
                UserName = "batman",
                Email = "batman@mail.com",
                NormalizedUserName = "batman",
                NormalizedEmail = "batman@MAIL.COM",
                
            };
            u1.PasswordHash = hasher.HashPassword(u1, "sofiane");
            u2.PasswordHash = hasher.HashPassword(u1, "batman");
            builder.Entity<User>().HasData(u1, u2);

           
            builder.Entity<Score>().HasData(
                new
                {
                    Id = 1,
                    Value = 8500,
                    IsPublic = true,
                    UserId = u1.Id 
                },
                new
                {
                    Id = 2,
                    Value = 7200,
                    IsPublic = false,
                    UserId = u1.Id 
                },
                new
                {
                    Id = 3,
                    Value = 9400,
                    IsPublic = true,
                    UserId = u2.Id 
                },
                new
                {
                    Id = 4,
                    Value = 6000,
                    IsPublic = false,
                    UserId = u2.Id 
                }
            );
        }


        public DbSet<Score> Score { get; set; } 
    }
}
