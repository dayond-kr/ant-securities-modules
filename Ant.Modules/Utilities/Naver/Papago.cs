using Newtonsoft.Json;

using RestSharp;

using ShareInvest.Entities.Naver;

using System.Net;

namespace ShareInvest.Utilities.Naver;

public class Papago(string clientId, string clientSecret) : RestClient("https://openapi.naver.com", configureDefaultHeaders: headers =>
{
    headers.Add("X-Naver-Client-Id", clientId);
    headers.Add("X-Naver-Client-Secret", clientSecret);
})
{
    public async Task<TranslateSyntax?> TranslateAsync(string detectLanguage, string sentence)
    {
        var request = new RestRequest("v1/papago/n2mt", Method.Post);

        request.AddParameter("source", detectLanguage);
        request.AddParameter("target", "en");
        request.AddParameter("text", sentence);

        var response = await ExecuteAsync(request, cts.Token);

        if (HttpStatusCode.OK == response.StatusCode && string.IsNullOrEmpty(response.Content) is false)
        {
            return JsonConvert.DeserializeObject<Entities.Naver.Papago>(response.Content).TranslateSyntax;
        }
        return null;
    }

    public async Task<string?> DetectLanguage(string sentence)
    {
        var request = new RestRequest("v1/papago/detectLangs", Method.Post);

        request.AddParameter("query", sentence);

        var response = await ExecuteAsync(request, cts.Token);

        if (HttpStatusCode.OK == response.StatusCode && string.IsNullOrEmpty(response.Content) is false)
        {
            return JsonConvert.DeserializeObject<DetectLangage>(response.Content).Langage;
        }
        return null;
    }

    readonly CancellationTokenSource cts = new();
}