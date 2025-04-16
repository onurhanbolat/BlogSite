
# 📝 BlogSite - ASP.NET Core MVC Blog Uygulaması

Bu proje, ASP.NET Core MVC kullanılarak geliştirilen katmanlı mimariye sahip bir blog platformudur. Modern arayüzü, kullanıcı/rol tabanlı erişim kontrolü ve özelleştirilebilir yapısıyla sade ama güçlü bir blog sistemi sunar.

---

## 🚀 Özellikler

✅ **Kullanıcı Kayıt / Giriş Sistemi** (ASP.NET Identity ile)

📝 **Blog Yönetimi**
- Blog oluşturma, düzenleme, silme
- Kategoriye göre filtreleme

📂 **Kategori Yönetimi**
- Kategori ekleme, düzenleme, silme (Admin paneli üzerinden)

💬 **Yorum Sistemi**
- Giriş yapan kullanıcılar yorum yapabilir
- Admin tüm yorumları görebilir ve silebilir
- Kullanıcılar sadece kendi yorumlarını silebilir

🔐 **Rol Tabanlı Yetkilendirme**
- `Admin`, `User` ve `Ziyaretçi` rollerine özel yetkiler
- Admin her içeriği görüntüleyebilir ve yönetebilir

📊 **Blog Okunma Sayısı Takibi**
- Cookie üzerinden ziyaretçi bazlı görüntülenme sayısı takibi

🌙 **Modern & Minimalist UI**
- Responsive tasarım (🎨 Bootstrap 5 ile)
- Temiz ve kullanıcı dostu ara yüz

🧑‍💻 **Admin Paneli**
- Tüm kullanıcıların bloglarını görüntüleyebilme
- Kullanıcılara `Admin` ↔ `User` rolü atayabilme
- Kategori yönetimi (oluşturma, düzenleme, silme)
- Tüm yorumlara erişim ve yönetim
- Kullanıcı listesi görüntüleme
- Yönetici olarak tüm içeriklere müdahale edebilme

---

## 🧱 Katmanlı Mimari (Layered Architecture)

Bu proje, **temiz kod** ve **sürdürülebilirlik** hedefiyle **katmanlı mimari** prensibine göre yapılandırılmıştır:

### 📁 `BlogSite.Web`
- Kullanıcı arayüzü (UI) katmanıdır.
- Tüm view, layout ve controller işlemleri burada yer alır.
- UI sadece servislerle konuşur, doğrudan veri katmanına erişmez.

### 📁 `BlogSite.Application`
- İş mantığının tanımlandığı katmandır.
- Tüm `Interface` tanımları ve `ViewModel`/`DTO` yapıları burada yer alır.
- `Loose Coupling` prensibi uygulanarak bağımlılıklar azaltılmıştır.

### 📁 `BlogSite.Domain`
- Projenin çekirdek katmanıdır.
- `Entity`, `Enum`, `Role` ve `Authorization` gibi domain öğeleri burada tanımlanır.
- Hiçbir katmana bağımlı değildir, sadece kendisini temsil eder.

### 📁 `BlogSite.Infrastructure`
- Veritabanı işlemleri ve dış bağımlılıklar bu katmanda çözülür.
- `DbContext`, `EF Core`, `Repository` ve `Seeder` işlemleri burada bulunur.
- Application katmanındaki interface'lerin somut karşılıkları bu katmanda yer alır.
