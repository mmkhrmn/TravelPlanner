using System.Threading.Tasks;

namespace TravelPlanner.Application.Interfaces
{
    /// <summary>
    /// Şehir, ülke ve kullanıcı tercihlerine göre yapay zeka destekli rota oluşturma işlemlerini yönetir.
    /// </summary>
    public interface IRouteService
    {
        /// <summary>
        /// Belirtilen lokasyon ve tercihler için Gemini üzerinden bir gezi planı hazırlar.
        /// </summary>
        /// <param name="cityName">Gidilecek şehir adı</param>
        /// <param name="countryName">Gidilecek ülke adı</param>
        /// <param name="wakeUpTime">Kullanıcının güne başlama saati (Örn: "09:00")</param>
        /// <param name="userNotes">Kullanıcının özel istekleri veya notları (Örn: "Vejetaryen mekanlar olsun")</param>
        /// <returns>Gemini API'den dönen, formatlanmış rota metni</returns>
        Task<string> GenerateRouteAsync(string cityName, string countryName, string wakeUpTime, string userNotes);
    }
}