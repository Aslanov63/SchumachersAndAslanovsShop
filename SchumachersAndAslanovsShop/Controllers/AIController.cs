using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;

namespace SchumachersAndAslanovsShop.Controllers
{
    public class AIController : Controller
    {
        // Твой ключ (держи его в секрете от посторонних!)
        private readonly string _apiKey = "AIzaSyDFXBZBm1lb5fdEKMeeT0Dya3_rKx4f8Xw";

        [HttpPost]
        public async Task<IActionResult> Ask([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message)) return Json(new { response = "Write something!" });

            // Модель Gemini 2.5 Flash - мощь 2026 года
            string model = "gemini-2.5-flash";
            string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={_apiKey}";

            using var client = new HttpClient();

            var requestBody = new
            {
                contents = new[] {
                    new { parts = new[] { new { text = $"You are an expert car assistant for Schumacher & Aslanov shop. Be helpful and professional. User question: {message}" } } }
                }
            };

            try
            {
                var response = await client.PostAsJsonAsync(url, requestBody);
                var jsonString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    // Обработка перегрузки (503) или лимитов (429)
                    if ((int)response.StatusCode == 503 || (int)response.StatusCode == 429)
                    {
                        return Json(new { response = "The AI garage is a bit crowded right now. Give me a second and try again! 🏎️💨" });
                    }
                    return Json(new { response = "Google API Error: " + jsonString });
                }

                using var doc = JsonDocument.Parse(jsonString);

                // Проверяем, что ответ не пустой и содержит текст
                if (doc.RootElement.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                {
                    var aiText = candidates[0]
                        .GetProperty("content")
                        .GetProperty("parts")[0]
                        .GetProperty("text")
                        .GetString();

                    return Json(new { response = aiText });
                }

                return Json(new { response = "AI is thinking, but couldn't find the right words." });
            }
            catch (Exception ex)
            {
                return Json(new { response = "Connection error: " + ex.Message });
            }
        }
    }
}