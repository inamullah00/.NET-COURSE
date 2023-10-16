

using Microsoft.EntityFrameworkCore;
using RESTAPI.Models.Domain;

namespace RESTAPI.Data

{
    public class NZWalksDbContext:DbContext
    {

        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options):base(options) 
        {
                
        }
        // DbSet<CollectionName> ----- myCollection etc like node const student = new model("modelName",studentSchema);
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var difficultyData = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id=Guid.Parse("2126ddec-1c42-454a-ade8-6a96df39bbba"),
                    Name="Easy"

                },

                new Difficulty()
                {
                    Id=Guid.Parse("43fcfd58-1b65-4ef0-8379-05131d3f7225"),
                    Name="Medium"

                },

                new Difficulty()
                {
                    Id=Guid.Parse("4932477d-cacf-4023-8109-d55e4a2b55cd"),
                    Name="Hard"

                }
            };

              //Seed Difficulty to the Database
            modelBuilder.Entity<Difficulty>().HasData(difficultyData);


            // Seed Data for Region

            var regionData = new List<Region>()
            {
                new Region()
                {
                    Id=Guid.Parse("0986b9f2-dbea-4a16-8419-476f2207c0f5"),
                    Code="AKL",
                    Name="IRLAND REGION",
                    RegionImageUrl="Some_image.jpg"
                },
                   new Region()
                {
                    Id=Guid.Parse("d8e9586b-ba4c-42f1-8bd4-9066bf70bae0"),
                    Code="DBI",
                    Name="DUBAI REGION",
                    RegionImageUrl="Some_image.jpg"
                },
                      new Region()
                {
                    Id=Guid.Parse("d8770156-0b55-4700-849a-b8713d9b2731"),
                    Code="IRL",
                    Name="ILAND STREET REGION",
                    RegionImageUrl="Some_image.jpg"
                },
            };

            modelBuilder.Entity<Region>().HasData(regionData);
        }
    }
}
