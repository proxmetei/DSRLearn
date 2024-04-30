namespace DSRLearn.Api.Controllers.Debts.Models
{
    public class RequestNettingDebtModel
    {
        public int Amount { get; set; }
        public Guid Id1 { get; set; }
        public Guid Id2 { get; set; }
    }
}
