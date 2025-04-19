namespace Taxually.TechnicalTest.Interfaces
{
    public interface IHttpClient
    {
        public Task PostAsync<TRequest>(string url, TRequest request);

    }
}
