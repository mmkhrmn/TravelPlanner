# TravelPlanner

TravelPlanner, kullanıcıların seyahat planlarını oluşturabileceği, düzenleyebileceği ve yönetebileceği ASP.NET Core tabanlı bir web uygulamasıdır.

## 🚀 Özellikler

- Seyahat planı oluşturma
- Plan düzenleme ve silme işlemleri
- ASP.NET Core MVC mimarisi
- Entity Framework Core kullanımı
- Responsive tasarım
- SQL Server veritabanı desteği
- SQLite desteği (alternatif kullanım)

---

# 🛠️ Kullanılan Teknolojiler

- ASP.NET Core
- C#
- Entity Framework Core
- SQL Server
- SQLite
- Razor Views
- Bootstrap

---

# 📦 Gereksinimler

Projeyi çalıştırabilmek için sisteminizde aşağıdakilerin kurulu olması gerekir:

- .NET SDK 8.0 veya üzeri
- SQL Server (varsayılan kullanım için)

.NET SDK indirmek için:

https://dotnet.microsoft.com/download

---

# ⚙️ Kurulum

## 1. Projeyi Klonlayın

```bash
git clone https://github.com/elifselmanmelih/TravelPlanner.git
```

```bash
cd TravelPlanner
```

---

# 🗄️ SQL Server Yapılandırması

`appsettings.json` dosyasında SQL Server bağlantı bilgilerinizi düzenleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=TravelPlannerDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## 2. Migration İşlemleri

Migration oluşturmak için:

```bash
dotnet ef migrations add InitialCreate
```

Veritabanını oluşturmak için:

```bash
dotnet ef database update
```

---

# ▶️ Projeyi Çalıştırma

```bash
dotnet run
```


# 🗄️ SQLite Kullanmak İsteyenler İçin

Proje varsayılan olarak SQL Server ile çalışmaktadır. Ancak daha hafif ve kurulum gerektirmeyen bir veritabanı sistemi kullanmak isteyenler için SQLite desteği kolayca eklenebilir.

SQLite, ekstra SQL Server kurulumu gerektirmeden tek bir `.db` dosyası ile çalışan hafif bir veritabanıdır.

---

## 1. SQLite Paketlerini Yükleyin

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

---

## 2. DbContext Yapılandırmasını Güncelleyin

`Program.cs` dosyasındaki SQL Server bağlantısını:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```

şununla değiştirin:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));
```

---

## 3. appsettings.json Dosyasını Düzenleyin

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=travelplanner.db"
  }
}
```

---

## 4. SQLite Migration Oluşturun

```bash
dotnet ef migrations add SqliteInit
```

---

## 5. SQLite Veritabanını Oluşturun

```bash
dotnet ef database update
```

Bu işlem sonunda proje klasörü içerisinde `travelplanner.db` adlı SQLite veritabanı dosyası oluşacaktır.

---

# 📁 Proje Yapısı

```text
TravelPlanner/
│
├── Controllers/
├── Models/
├── Views/
├── Data/
├── wwwroot/
├── appsettings.json
├── Program.cs
└── TravelPlanner.csproj
```

---

# 💾 SQLite Veritabanını Görüntüleme

SQLite veritabanını görüntülemek için aşağıdaki araçlardan biri kullanılabilir:

- DB Browser for SQLite
- SQLiteStudio

DB Browser for SQLite:

https://sqlitebrowser.org/

---

# Görseller
