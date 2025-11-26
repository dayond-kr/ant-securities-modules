using Newtonsoft.Json.Linq;

using RestSharp;

using System.Net;
using System.Text;

namespace ShareInvest.Utilities.Naver;

public enum Services
{
    blog,
    news,
    book,
    encyc,
    cafearticle,
    kin,
    webkr,
    image,
    shop,
    doc,
    adult,
    errata
}

public class Search(string clientId, string clientSecret) : RestClient("https://openapi.naver.com", configureDefaultHeaders: headers =>
{
    headers.Add("X-Naver-Client-Id", clientId);
    headers.Add("X-Naver-Client-Secret", clientSecret);
})
{
    public async Task<string?> SearchAsync(JToken query, Services id = Services.news)
    {
        var resource = Parameter.TransformQuery(query, new StringBuilder(string.Concat(route, '/', id)));

        var response = await ExecuteAsync(new RestRequest(resource), cts.Token);

        return HttpStatusCode.OK == response.StatusCode && !string.IsNullOrEmpty(response.Content) ? response.Content : null;
    }

    readonly CancellationTokenSource cts = new();

    const string route = "v1/search";
}