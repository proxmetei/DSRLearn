

namespace DSRLearn.Context.Entities
{
    public class Debt: BaseEntity
    {
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public virtual User Debtor { get; set; }
        public virtual User Creditor { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
}
