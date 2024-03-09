using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Context
{
    public static class UserDetailsContextConfiguration
    {
        public static void ConfigureUserDetails(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetail>().ToTable("user_details");
            modelBuilder.Entity<UserDetail>().HasOne(x => x.User).WithOne(x => x.Detail).HasPrincipalKey<UserDetail>(x => x.Id);
        }
    }
}
