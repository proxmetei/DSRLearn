namespace DSRLearn.Web.Pages.Debts.Models
{
    public class CreateDebtModel
    {
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
}
