namespace DSRLearn.Services.Actions;

using System.Threading.Tasks;

public interface IAction
{
    Task SendMessage(SendMeassageModel model);
}
