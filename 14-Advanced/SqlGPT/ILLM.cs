using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace SqlGPT
{
    public interface ILLM
    {
        public void Init(string apikey);
        public Task<string> RunQueryAsync(string prompt);
    }
    internal class GeminiClient : ILLM
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string apikey;
        private string url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent";

        public void Init(string apikey)
        {
            this.apikey = apikey;
            this.url += $"?key={apikey}";
        }

        public async Task<string> RunQueryAsync(string prompt)
        {
            if (_httpClient == null)
            {
                throw new Exception("Client is not initialized");
            }
            string body = BuildRequestBody(prompt);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _httpClient.PostAsync(this.url, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseBody);
            var result = jsonObject["candidates"][0]["content"]["parts"][0]["text"].ToString().Trim();
            return result;
        }
        private static string BuildRequestBody(string text)
        {
            var jsonObject = new
            {
                contents = new[]
             {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = text
                        }
                    }
                }
            },
                safetySettings = new[]
                {
                    new{
                        category = "HARM_CATEGORY_DANGEROUS_CONTENT",
                        threshold = "BLOCK_ONLY_HIGH"
                    }
                },
                generationConfig = new
                {
                    maxParts = 1,
                    maxOutputTokens = 100,
                    temperature = 0.01,
                    //topP = 1,
                    //topK = 10,
                }
            };
            string jsonBody = JsonConvert.SerializeObject(jsonObject);
            return jsonBody;
        }

    }
    internal class OpenAIClient : ILLM
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private static string GPT_MODEL = "gpt-3.5-turbo";//move to configuaration
        public void Init(string apikey)
        {
            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/chat/completion");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apikey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> RunQueryAsync(string prompt)
        {
            if (_httpClient == null)
            {
                throw new Exception("Client is not initialized");
            }
            var body = JsonConvert.SerializeObject(BuildRequestBody(prompt));
            var response = await _httpClient.PostAsync("completions",
                new StringContent(body, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseBody);
            var result = jsonObject["choices"][0]["message"]["content"].ToString().Trim();
            return result;

        }
        private static object BuildRequestBody(string p)
        {
            var requestBody = new
            {
                model = GPT_MODEL,
                messages = new[]{
                    new{
                        role="user",
                        content=p
                    },
                },
                max_tokens = 150,
                //temperature=0.1,
                //top_p=1
            };
            return requestBody;
        }

    }
}
