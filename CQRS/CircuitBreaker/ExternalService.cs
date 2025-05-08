
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CQRS.CircuitBreaker
{
    public record AzureADToken
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonPropertyName("ext_expires_in")]
        public string ExtExpiresIn { get; set; }

        [JsonPropertyName("expires_on")]
        public string ExpiresOn { get; set; }

        [JsonPropertyName("not_before")]
        public string NotBefore { get; set; }

        public string Resource { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }
    public class ExternalService : IExternalService
    {
        const string clientId = "aa6ff581-3faf-49df-9ca4-b7076b0c760e";
        //const string clientSecret = 'YOUR_CLIENT_SECRET';
        const string secret = "TTM8Q~Xd-3Bp5k.xImeB.NKvlIlKtF8oUcprWaq6";
        const string tenantId = "8eb87a6e-8055-4135-b69d-f19c799ec045";
        const string functionUrl ="https://funclogic49387723.azurewebsites.net/api/GetSettingInfo";
        //const string functionUrl1 = "https://funclogic49387723.azurewebsites.net/api/GetSettingInfo?code=FK2DXgkU3qrrUx166Rz1wOW2zf4EOSS4tSqU28nCPfw2AzFubRFrDQ==";
        const string tokenUrl = "https://login.microsoftonline.com/8eb87a6e-8055-4135-b69d-f19c799ec045/oauth2/v2.0/token/";
        const string OathUrl = "https://login.microsoftonline.com/8eb87a6e-8055-4135-b69d-f19c799ec045/oauth2/v2.0/authorize";
        const string resourceUrl = "https://funclogic49387723.azurewebsites.net/api/GetSettingInfo/";
        const string requestUrl = $"https://login.microsoftonline.com/{tenantId}/oauth2/token";
        const string requestUrl1 = $"{tenantId}/oauth2/token";
        async Task<string> IExternalService.GetDataAsync()
        {
            try
            {
                Console.WriteLine("This is the external service");
                int a = 10, b = 0;
                //int c = a / b;
                await Task.Delay(10);
                var dict = new Dictionary<string, string>(){
                { "grant_type", "client_credentials" },
                { "client_id", clientId },
                { "client_secret", secret },
                //{ "resource", functionUrl1 }
                };
                var token = string.Empty;
                var requestBody = new FormUrlEncodedContent(dict);
                var requestBody1 = new FormUrlEncodedContent(dict);
                var scope = $"api://aa6ff581-3faf-49df-9ca4-b7076b0c760e/.default";
                var scopes = new[] { scope };
                using (HttpClient httpClient=new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://login.microsoftonline.com/");

                    //var response = await httpClient.PostAsync(requestUrl1, requestBody);
                    var response = await httpClient.PostAsync(requestUrl1, requestBody);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var aadToken = JsonSerializer.Deserialize<AzureADToken>(responseContent);
                    token = aadToken?.AccessToken;
                    Console.WriteLine(aadToken?.AccessToken);
                }
                var app = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(secret)            
                .Build();
                AuthenticationResult result = await app.AcquireTokenForClient(scopes)
                                    .ExecuteAsync();
                var client = new HttpClient();
                //Console.WriteLine($"Calling {functionUrl1}");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", result.AccessToken);
                //string json = await client.GetStringAsync(functionUrl1);
                return "This is the external service - "+ token;
                    }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}
