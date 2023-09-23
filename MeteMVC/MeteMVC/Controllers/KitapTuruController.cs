using Microsoft.AspNetCore.Mvc;
using MeteMVC.Models;
using MeteMVC.Utility;

namespace MeteMVC.Controllers
{
    public class KitapTuruController : Controller
    {


        private readonly IKitapTuruRepository _kitapTuruRepository;   //program.cs nin içindeki service kodundan kaynaklı newlememize gerek yok .net bizim için konteynırda bu elemanı tutuyor DEPENDENCY INJECTION soli(D)
        public KitapTuruController(IKitapTuruRepository context)
        {
            _kitapTuruRepository = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();  //SQL İN İÇERSİNDEKİ VERİLERİ ÇEKTİ MVC NİN MODEL KISMI 
            return View(objKitapTuruList);   // Viewe göndermek için yaptık


        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            //bu backend ile validation yazdırma HATA MESAJLARI
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Ekle(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Oluşturuldu!";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVT = _kitapTuruRepository.Get(u => u.Id == id); //public T Get(Expression<Func<T, bool>> filtre) Repositorunin içine bak
            if (kitapTuruVT == null)
            {
                return NotFound();
            }
            return View(kitapTuruVT);
        }
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Guncelle(kitapTuru);
                _kitapTuruRepository.Kaydet();
                TempData["basarili"] = "Kitap Türü Başarıyla Güncellendi!";
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVT = _kitapTuruRepository.Get(u => u.Id == id);
            if (kitapTuruVT == null)
            {
                return NotFound();
            }
            return View(kitapTuruVT);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int id)
        {
            KitapTuru? kitapTuru = _kitapTuruRepository.Get(u => u.Id == id);
            if (kitapTuru == null)
            {
                return NotFound();
            }
            _kitapTuruRepository.Sil(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "Kitap Türü Başarıyla Silindi!";
            return RedirectToAction("Index", "KitapTuru");
        }
    }
}

