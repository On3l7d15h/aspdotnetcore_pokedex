using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//added
using Microsoft.EntityFrameworkCore;
using Database.Models;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        //constructor method
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        // Creating the DbSets!
        #region DbSets
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Database.Models.Type> Types { get; set; }

        #endregion

        //Updating the OnModelCreating Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Creating and give a name to tables.
            #region Tables
            modelBuilder.Entity<Pokemon>().ToTable("Pokemons");
            modelBuilder.Entity<Region>().ToTable("Regions");
            modelBuilder.Entity<Database.Models.Type>().ToTable("Types");
            #endregion

            // Creating the primary keys
            #region Primary Keys
            modelBuilder.Entity<Pokemon>().HasKey(pkmn => pkmn.Id);
            modelBuilder.Entity<Region>().HasKey(r => r.Id);
            modelBuilder.Entity<Database.Models.Type>().HasKey(t => t.Id);
            #endregion

            // Making The Properties
            #region Set Constraints

                //Pokemon Table Properties
                #region Pokemons
                modelBuilder.Entity<Pokemon>()
                    .Property(pkmn => pkmn.Name)
                    .IsRequired();

                modelBuilder.Entity<Pokemon>()
                    .Property(pkmn => pkmn.ImageUrl)
                    .IsRequired();

                modelBuilder.Entity<Pokemon>()
                    .Property(pkmn => pkmn.PrimaryTypeId)
                    .IsRequired();

                modelBuilder.Entity<Pokemon>()
                    .Property(pkmn => pkmn.RegionId)
                    .IsRequired();
                #endregion

                //Region Table Properties
                #region Regions
                modelBuilder.Entity<Region>()
                    .Property(r => r.Name)
                    .IsRequired();
                #endregion

                //Type Table Properties
                #region Types
                modelBuilder.Entity<Database.Models.Type>()
                   .Property(t => t.Name)
                   .IsRequired();
            #endregion

            #endregion

            // Relationships
            #region RelationShips

                // Type Relationship
                #region Type RelationShip
                modelBuilder.Entity<Database.Models.Type>()
                    .HasMany<Pokemon>(t => t.Pokemons)
                    .WithOne(pkmn => (Database.Models.Type)pkmn.Type)
                    .HasForeignKey(p => p.PrimaryTypeId)
                    .OnDelete(DeleteBehavior.Cascade);

                #endregion

            //Region Relationship
            #region Region Relationship
            modelBuilder.Entity<Region>()
                        .HasMany<Pokemon>(t => t.Pokemons)
                        .WithOne(pkmn => pkmn.Region)
                        .HasForeignKey(p => p.RegionId)
                        .OnDelete(DeleteBehavior.Cascade);
                #endregion

            #endregion
        }
    }
}
