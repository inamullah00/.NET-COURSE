using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RESTAPI.Data
{
    public class NZWalksAuthDBContext:IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options):base(options) { 
        
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "72f398f2-e19c-4be8-be72-85b19d2b5828";
            var writerRoleId = "6d88e068-873f-40e5-97ba-c1559700c514";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader" ,
                    NormalizedName="Reader".ToUpper(),

              } ,
                 new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer" ,
                    NormalizedName="Writer".ToUpper(),

              }
            };

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
