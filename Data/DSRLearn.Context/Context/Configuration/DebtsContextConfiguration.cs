using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace DSRLearn.Context
{
    public static class DebtsContextConfiguration
    {
        public static void ConfigureDebts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Debt>().ToTable("debts");
            modelBuilder.Entity<Debt>().Property(x => x.Amount).IsRequired();
            modelBuilder.Entity<Debt>().Property(x => x.RepaidDate).IsRequired();
            modelBuilder.Entity<Debt>().HasOne(x => x.Creditor).WithMany(x => x.DebtsIssued).HasForeignKey(x => x.CreditorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Debt>().HasOne(x => x.Debtor).WithMany(x => x.DebtsRecieved).HasForeignKey(x => x.DebtorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
