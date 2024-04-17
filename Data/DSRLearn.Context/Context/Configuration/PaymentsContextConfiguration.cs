using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Context
{
    public static class PaymentsContextConfiguration
    {
        public static void ConfigurePayments(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().ToTable("payments");
            modelBuilder.Entity<Payment>().Property(x => x.Amount).IsRequired();
            modelBuilder.Entity<Payment>().Property(x => x.Date).IsRequired();
            modelBuilder.Entity<Payment>().HasOne(x => x.Debt).WithMany(x => x.Payments).HasForeignKey(x => x.DebtId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
