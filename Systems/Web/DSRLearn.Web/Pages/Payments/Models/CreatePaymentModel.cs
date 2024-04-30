namespace DSRLearn.Web.Pages.Payments.Models
{
    public class CreatePaymentModel
    {
        public Guid DebtId { get; set; }
        public int Amount { get; set; }
        public DateOnly Date { get; set; }
    }
}
