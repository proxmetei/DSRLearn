namespace DSRLearn.Worker;

using DSRLearn.Services.RabbitMq;
using MimeKit;
using MailKit.Net.Smtp;
using DSRLearn.Services.Actions;
using DSRLearn.Services.Logger;

public class TaskExecutor : ITaskExecutor
{
    private readonly IAppLogger logger;
    private readonly IRabbitMq rabbitMq;

    public TaskExecutor(
        IAppLogger logger,
        IRabbitMq rabbitMq
    )
    {
        this.logger = logger;
        this.rabbitMq = rabbitMq;
    }

    public void Start()
    {
        
        rabbitMq.Subscribe<SendMeassageModel>(QueueNames.SEND_MESSAGE, async data =>
        {
            logger.Information($"Starting sending of message::: {data}");

        var emailMessage = new MimeMessage();

            try
            {
                emailMessage.From.Add(new MailboxAddress("Администрация сайта", "aleksei.korchagin200@mail.ru"));
                emailMessage.To.Add(new MailboxAddress("", data.Email));
                emailMessage.Subject = data.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = data.Message
                };
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                
                await client.AuthenticateAsync("aleksei.korchagin200@mail.ru", "M0FWfGSXyrMbgeSmrah7");
                
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);

                logger.Information($"Message was send::: {data.Email} | {data.Subject}");
            }
        });
    }
}