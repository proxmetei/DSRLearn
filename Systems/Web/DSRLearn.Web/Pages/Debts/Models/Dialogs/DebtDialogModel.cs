namespace DSRLearn.Web.Pages.Debts.Models
{
    public class DebtDialogModel
    {
        public Guid Id { get; set; }
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public int Amount { get; set; }
        public DateTime? RepaidDate { get; set; }
    }
}
