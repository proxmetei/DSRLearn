namespace DSRLearn.Web.Pages.Payments.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid DebtId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
}
