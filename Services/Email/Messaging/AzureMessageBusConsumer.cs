using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

using EmailService.Models;
using EmailService.Service;


namespace EmailService.Messaging
{
    public class AzureMessageBusConsumer:IAzureMessageBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly string Connectionstring;
        private readonly string QueueName;
        private readonly ServiceBusProcessor _registrationProcessor;
        private readonly EmailSendService _emailService;
        public AzureMessageBusConsumer(IConfiguration configuration )
        {

            _configuration = configuration;
            Connectionstring= _configuration.GetSection("ServiceBus:ConnectionString").Get<string>();

            QueueName= _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();

            var serviceBusClient = new ServiceBusClient(Connectionstring);
            _registrationProcessor = serviceBusClient.CreateProcessor(QueueName);
            _emailService = new EmailSendService();
          

        }

        public async Task Start()
        {
            //start Processing
            _registrationProcessor.ProcessMessageAsync += OnRegistration;
            _registrationProcessor.ProcessErrorAsync += ErrorHandler;
            await _registrationProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            //Stop Processing
           await  _registrationProcessor.StopProcessingAsync();
           await _registrationProcessor.DisposeAsync();
        }
        private Task ErrorHandler(ProcessErrorEventArgs arg)
        {   

            //Todo send an email to Admin

           throw new NotImplementedException();
        }

        private async Task OnRegistration(ProcessMessageEventArgs arg)
        {
            var message= arg.Message;

            var body = Encoding.UTF8.GetString(message.Body);

            var userMessage= JsonConvert.DeserializeObject<UserMessage>(body);

            //sending An Email
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<img src=\"https://cdn.pixabay.com/photo/2020/03/21/10/40/bmw-4953337_640.png");
                stringBuilder.Append("<h1> Hello " + userMessage.Name + "</h1>");
                stringBuilder.AppendLine("<br/>Welcome to The Jitu Shopping Site ");

                stringBuilder.Append("<br/>");
                stringBuilder.Append('\n');
                stringBuilder.Append("<p> Start Shopping here</p>");
                await _emailService.sendEmail(userMessage, stringBuilder.ToString());
                //delete the message from the queue
                 await arg.CompleteMessageAsync(message);
            }catch (Exception ex) { }
        }

       
    }
}