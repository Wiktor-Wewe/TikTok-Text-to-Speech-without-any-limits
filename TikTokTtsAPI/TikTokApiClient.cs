using RestSharp;
using System.Text.Json;
using TikTokTtsAPI.Deserialization;

namespace TikTokTtsAPI
{
    public class TikTokApiClient : IDisposable
    {
        private RestClient _restClient { get; set; }

        public TikTokApiClient()
        {
            var options = new RestClientOptions("https://tiktok-tts.weilnet.workers.dev")
            {
                MaxTimeout = -1,
            };
            _restClient = new RestClient(options);
        }

        public async Task<byte[]> SendRequestAndGetData(string text, string voice)
        {
            var request = new RestRequest("https://tiktok-tts.weilnet.workers.dev/api/generation", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = $"{{\"text\": \"{text}\", \"voice\": \"{voice}\"}}";
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Response from api wasn't success.");
            }

            if (response.Content == null)
            {
                throw new Exception("Response.Content was null.");
            }

            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(response.Content);

            if (apiResponse == null)
            {
                throw new Exception("Deserialization was failed.");
            }

            if(apiResponse.success == false) 
            {
                throw new Exception("Response from api after deserialization wasn't success.");
            }

            return Convert.FromBase64String(apiResponse.data);
        }

        public void Dispose()
        {
            _restClient.Dispose();
        }
    }
}
