namespace Resilience.Http.DependencyInjection
{
    public interface IResilienceHttpClientFactory
    {
        ResilienceHttpClient CreateResilienceHttpClient();
    }
}
