using System.Net.Http.Json;
using DSRLearn.Web.Pages.Debts.Models;
using DSRLearn.Web.Pages.Payments.Models;

namespace DSRLearn.Web.Pages.Debts.Services;

public class DebtService(HttpClient httpClient) : IDebtService
{
    public async Task<IEnumerable<DebtModel>> GetAll()
    {
        var response = await httpClient.GetAsync("v1/Debt");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<DebtModel>>() ?? new List<DebtModel>();
    }
    public async Task<IEnumerable<DebtModel>> GetDebts()
    {
        var response = await httpClient.GetAsync("v1/Debt/GetDebts");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<DebtModel>>() ?? new List<DebtModel>();
    }
    public async Task<IEnumerable<DebtModel>> GetCredits()
    {
        var response = await httpClient.GetAsync("v1/Debt/GetCredits");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<IEnumerable<DebtModel>>() ?? new List<DebtModel>();
    }
    public async Task<DebtModel> GetDebt(Guid debtId)
    {
        var response = await httpClient.GetAsync($"v1/Debt/{debtId}");
        if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }

        return await response.Content.ReadFromJsonAsync<DebtModel>() ?? new();
    }

    public async Task AddDebt(CreateDebtModel model)
    {

        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PostAsync("v1/Debt", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task EditDebt(UpdateDebtModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/Debt", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
    public async Task NettingDebt(CreateNettingModel model)
    {
        var requestContent = JsonContent.Create(model);
        var response = await httpClient.PutAsync($"v1/Debt/CreateNetting", requestContent);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }

    public async Task DeleteDebt(Guid debtId)
    {
        var response = await httpClient.DeleteAsync($"v1/Debt/{debtId}");

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
    }
}