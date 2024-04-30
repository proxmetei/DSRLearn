using System.Net.Http.Json;
using DSRLearn.Web.Pages.Payments.Models;

namespace DSRLearn.Web.Pages.Payments.Services
{
    public class PaymentService(HttpClient httpClient) : IPaymentService
    {
        public async Task<IEnumerable<PaymentModel>> GetByDebt(Guid debtId)
        {
            var response = await httpClient.GetAsync($"v1/Payment/GetByDebtId/{debtId}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            return await response.Content.ReadFromJsonAsync<IEnumerable<PaymentModel>>() ?? new List<PaymentModel>();
        }
        public async Task AddPayment(CreatePaymentModel model)
        {

            var requestContent = JsonContent.Create(model);
            var response = await httpClient.PostAsync("v1/Payment", requestContent);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }
        }
    }
}
