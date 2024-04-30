namespace DSRLearn.Web.Pages.Debts.Models
{
    public class UpdateDebtModel
    {
        public int Amount { get; set; }
        public Guid Id { get; set; }
        public DateOnly RepaidDate { get; set; }
    }
}
