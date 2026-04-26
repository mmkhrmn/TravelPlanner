using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TravelPlanner.Application.Interfaces;

namespace TravelPlanner.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public RouteService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<string> GenerateRouteAsync(string cityName, string countryName, string wakeUpTime, string userNotes)
        {
            // 1. API Key'i SADECE appsettings'den çekiyoruz. Kodun içinde şifre yok!
            var apiKey = _config["Gemini:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "BURAYA_GIRILMEYECEK" || apiKey == "API_KEY_BURAYA_GIRILMEYECEK")
                throw new InvalidOperationException("Gemini API key bulunamadı. Lütfen appsettings.Development.json dosyasını kontrol edin.");

            // 2. Gelen verileri kontrol etme ve varsayılan değer atama
            // Eğer kullanıcı saat seçmediyse varsayılan olarak 09:00 atıyoruz.
            string startTime = string.IsNullOrWhiteSpace(wakeUpTime) ? "09:00" : wakeUpTime;
            
            // Eğer kullanıcı not girdiyse bunu prompt'a ekleyecek özel bir cümle oluşturuyoruz.
            string extraNotesInstruction = string.IsNullOrWhiteSpace(userNotes) 
                ? "" 
                : $"DİKKAT KULLANICI NOTU: Şu özel isteklere mutlaka uy ve rotayı buna göre şekillendir: '{userNotes}'. ";

            // 3. Prompt Hazırlama (Dinamik ve katı kurallı)
            var prompt = $"Sen {cityName}, {countryName} bölgesini avucunun içi gibi bilen profesyonel bir rehbersin. " +
                         $"Saat {startTime}'dan başlayıp akşam 22:00'yi içerecek şekilde saat saat planlanmış, detaylı ve gerçek mekan isimleri içeren bir günlük gezi rotası hazırla. " +
                         $"{extraNotesInstruction}" +
                         $"DİKKAT: Hiçbir selamlama, giriş veya kapanış cümlesi kurma. Sadece '{startTime} - Aktivite' formatında rotayı yaz. 22:00 aktivitesini yazdıktan sonra metni anında bitir ve başka hiçbir şey ekleme.";

            // 4. Endpoint Ayarı (En kararlı çalışan 2.5-flash sürümü kullanıldı)
            var endpoint = $"https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key={apiKey}";

            // 5. Request Body Oluşturma
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[] { new { text = prompt } }
                    }
                },
                generationConfig = new
                {
                    temperature = 0.7, 
                    maxOutputTokens = 8192 // Metnin yarım kalmaması için sınırı genişlettik
                }
            };

            try
            {
                var jsonBody = JsonSerializer.Serialize(requestBody);
                using var httpContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // 6. API İsteği
                using var resp = await _httpClient.PostAsync(endpoint, httpContent);
                var respJson = await resp.Content.ReadAsStringAsync();

                if (!resp.IsSuccessStatusCode)
                {
                    return $"API Hatası Çıktı: {resp.StatusCode} - Detay: {respJson}";
                }

                // 7. JSON Yanıtını Ayıklama (Gelen tüm parçaları birleştirerek okuma)
                using var doc = JsonDocument.Parse(respJson);
                var root = doc.RootElement;

                if (root.TryGetProperty("candidates", out var candidates) && 
                    candidates.ValueKind == JsonValueKind.Array && 
                    candidates.GetArrayLength() > 0)
                {
                    var firstCandidate = candidates[0];
                    if (firstCandidate.TryGetProperty("content", out var content) && 
                        content.TryGetProperty("parts", out var parts) && 
                        parts.ValueKind == JsonValueKind.Array)
                    {
                        var sbText = new StringBuilder();
                        foreach (var part in parts.EnumerateArray())
                        {
                            if (part.TryGetProperty("text", out var textElement))
                            {
                                sbText.Append(textElement.GetString());
                            }
                        }
                        
                        var textContent = sbText.ToString();
                        return string.IsNullOrWhiteSpace(textContent) ? BuildMockItinerary(cityName) : textContent;
                    }
                }
            }
            catch (Exception ex)
            {
                // Bir hata oluşursa yedek rotayı dönüyoruz
                Console.WriteLine($"Hata: {ex.Message}");
            }

            return BuildMockItinerary(cityName);
        }

        /// <summary>
        /// API çalışmadığında veya hata verdiğinde kullanıcıya boş ekran göstermemek için yedek rota üretir.
        /// </summary>
        private static string BuildMockItinerary(string city)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"--- {city} İçin Örnek Günlük Rota (Yedek Veri) ---");
            sb.AppendLine();
            var activities = new[]
            {
                "09:00 - Kahvaltı: Şehir merkezinde yerel bir fırında güne başlangıç.",
                "11:00 - Tarih Turu: En yakın müze veya tarihi yapının ziyareti.",
                "13:00 - Öğle Yemeği: Meşhur yerel bir restoranda mola.",
                "15:00 - Keşif: Şehrin popüler parklarında veya sahil şeridinde yürüyüş.",
                "17:00 - Kahve Molası: Butik bir kafede kısa bir dinlenme.",
                "19:00 - Akşam Yemeği: Manzaralı bir mekanda günü sonlandırma."
            };

            foreach (var a in activities)
                sb.AppendLine(a);

            return sb.ToString();
        }
    }
}