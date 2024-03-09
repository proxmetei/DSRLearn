

namespace DSRLearn.Context.Entities
{
    public class Payment: BaseEntity
    {
        public int? DebtId { get; set; }
        public virtual Debt Debt { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
}
