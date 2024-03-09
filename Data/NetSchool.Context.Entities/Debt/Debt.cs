

namespace DSRLearn.Context.Entities
{
    public class Debt: BaseEntity
    {
        public int? DebtorId { get; set; }
        public int? CreditorId { get; set; }
        public virtual User Debtor { get; set; }
        public virtual User Creditor { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
}
