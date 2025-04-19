namespace Taxually.TechnicalTest.Interfaces
{
    public interface IQueueClient
    {
        public Task EnqueueAsync<TPayload>(string queueName, TPayload payload);
    }
}
