namespace Rewards.Messaging
{
    public interface IAzureMessageBusConsumer
    {
        Task Start();


        Task Stop();
    }
}