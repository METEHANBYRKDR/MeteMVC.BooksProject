using Microsoft.EntityFrameworkCore;
using MeteMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MeteMVC.Utility


{
    // veri tabanında EF Tablo oluşturması için ilgili model sınıflarınızı buraya eklemelisiniz.

    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}
