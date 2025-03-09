using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace bg.hackathon.alphahackers.application.data.interfaces.services
{
    public interface IHttpRequestService
    {
        Task<TDestination> ExecuteRestRequestAsync<TSource, TDestination>(
        string url,
        HttpMethod method,
        Dictionary<string, string>? headers = null,
        Dictionary<string, string>? queryParams = null,
        object content = null,
        int timeoutMilliseconds = 10000,
        Func<TSource, TDestination>? mapFunction = null,
        JsonSerializerSettings? jsonSettings = null,
        [CallerMemberName] string? callerName = null);
    }
}
