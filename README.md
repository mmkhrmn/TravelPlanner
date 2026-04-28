# ✈️ Seyahat Planlayıcı (TravelPlanner)

Gideceğiniz ülkeyi, şehri, güne başlama saatinizi ve özel isteklerinizi belirterek saniyeler içinde size özel, saat saat planlanmış bir seyahat programı elde edebilirsiniz.

![Proje Durumu](https://img.shields.io/badge/Durum-Geliştirme_Aşamasında-success)
![Versiyon](https://img.shields.io/badge/Versiyon-1.0.0-blue)

---

## ✨ Özellikler

* **🤖 Yapay Zeka Destekli Rota:** Kullanıcı tercihlerine göre optimize edilmiş, akıllı ve gerçekçi günlük planlar.
* **🌍 Dinamik Veri Çekimi:** Ülke ve şehir listeleri arka plandaki .NET API'den anlık olarak beslenir.
* **🎨 Modern ve Zarif Arayüz (UI/UX):** Apple/Airbnb tasarım dilinden ilham alınmış, cam efekti (glassmorphism) ve yumuşak animasyonlarla desteklenmiş premium tasarım.
* **⏱️ İnteraktif Zaman Çizelgesi (Timeline):** Oluşturulan rotanın kullanıcıyı yormayan, görsel olarak zenginleştirilmiş bir çizelge ile sunulması.
* **📱 Tam Uyumlu (Responsive):** Mobil cihazlarda alt alta, geniş ekranlarda yan yana (Dashboard) kusursuz görünüm.

---

## 🛠️ Kullanılan Teknolojiler

**Frontend (Ön Yüz)**
* HTML5, CSS3, Vanilla JavaScript
* Bootstrap 5.3.2
* Bootstrap Icons
* Google Fonts (Poppins)

**Backend (Arka Yüz)**
* C# / .NET Core 8.0 (Web API)
* Entity Framework Core (ORM)
* SQL Server / SQLite (Veritabanı)
* Yapay Zeka Entegrasyonu (LLM)

---

## 🚀 Kurulum Rehberi (Adım Adım)

Projeyi başka bir bilgisayarda sıfırdan çalıştırmak için bu adımları izleyin.

### 1. Projeyi Klonlayın ve Paketleri İndirin
Kodu bilgisayarınıza indirdikten sonra terminali açın ve gerekli tüm kütüphaneleri (NuGet paketlerini) sisteme yükleyin:

```bash
git clone [https://github.com/elifselmanmelih/TravelPlanner.git](https://github.com/elifselmanmelih/TravelPlanner.git)
cd TravelPlanner/TravelPlanner.API
dotnet restore
```
2. Kritik Yapılandırma (ApiKey ve Veritabanı)
appsettings.json dosyasını bir editörle açın ve aşağıdaki iki alanı kendinize göre güncelleyin. Bu adım olmadan uygulama rota oluşturamaz ve veritabanına bağlanamaz:
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=KENDI_SERVER_ADINIZ;Database=TravelPlannerDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "AiSettings": {
    "ApiKey": "BURAYA_KENDI_API_KEYINIZI_YAZIN"
  }
}
```
3. Veritabanını Oluşturun
SQL Server üzerinde gerekli tabloların ve örnek verilerin (şehirler/ülkeler) oluşması için şu komutu çalıştırın:
```bash
dotnet ef database update
```
4. Uygulamayı Başlatın
Backend: Terminale dotnet run yazın. API http://localhost:5146 adresinde çalışacaktır.

Frontend: index.html dosyasını VS Code Live Server eklentisi ile açın.
SQL Server Kurulu Değilse (Hızlı Çözüm)
Eğer SQL Server kurulu olmayan bir bilgisayarda çalıştıracaksanız, SQLite kullanarak projeyi hızlıca "Tak-Çalıştır" hale getirebilirsiniz:

1. Paketleri İndirin:

```bash 
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```
2. Ayar Değiştirin: appsettings.json içindeki bağlantıyı aşağıdaki gibi yapın:
```bash 
JSON
"ConnectionStrings": {
  "DefaultConnection": "Data Source=TravelPlanner.db"
}
```
3. Kodu Güncelleyin: Program.cs içinde .UseSqlServer yerine .UseSqlite yazın.

4. Veritabanı İnşa Edin: Mevcut Migrations klasörünü silip sırasıyla şu komutları çalıştırın:

Bash
dotnet ef migrations add Initial
dotnet ef database update
