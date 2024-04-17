using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Context
{
    public static class UserProfileContextConfiguration
    {
        public static void ConfigureUserDetails(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>().ToTable("user_profiles");
            modelBuilder.Entity<UserProfile>().HasOne(x => x.User).WithOne(x => x.Profile).HasPrincipalKey<UserProfile>(x => x.Id); ;
        }
    }
}