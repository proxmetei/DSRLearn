namespace DSRLearn.Services.Actions;

using DSRLearn.Services.RabbitMq;
using System.Threading.Tasks;

public class Action : IAction
{
    private readonly IRabbitMq rabbitMq;

    public Action(IRabbitMq rabbitMq)
    {
        this.rabbitMq = rabbitMq;
    }

    public async Task SendMessage(SendMeassageModel model)
    {
        await rabbitMq.PushAsync(QueueNames.SEND_MESSAGE, model);
    }
}
