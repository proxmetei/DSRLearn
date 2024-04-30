using DSRLearn.Web.Pages.Payments.Models;
using DSRLearn.Web.Pages.Users.Models;

namespace DSRLearn.Web.Pages.Debts.Models
{
    public class DebtModel
    {
        public Guid Id { get; set; }
        public Guid DebtorId { get; set; }
        public Guid CreditorId { get; set; }
        public UserModel Debtor { get; set; }
        public UserModel Creditor { get; set; }
        public int Amount { get; set; }
        public DateOnly RepaidDate { get; set; }
        public IEnumerable<PaymentModel> Payments { get; set; }
    }
}
