using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeteMVC.Models;
using MeteMVC.Utility;

namespace MeteMVC.Controllers
{
    public class KitapController : Controller
    {

        private readonly IKitapRepository _kitapRepository;   //program.cs nin içindeki service kodundan kaynaklı newlememize gerek yok .net bizim için konteynırda bu elemanı tutuyor DEPENDENCY INJECTION soli(D)
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;



        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();  //SQL İN İÇERSİNDEKİ VERİLERİ ÇEKTİ MVC NİN MODEL KISMI !!!1değişiklik include metoduyla kitap daki foreign keyleri getirdik KitapTuru,KitapTuruId  
            return View(objKitapList);   // Viewe göndermek için yaptık


        }
        public IActionResult EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()
            });
            ViewBag.KitapTuruList = KitapTuruList;
            if (id == null || id == 0)
            {
                //ekle
                return View();
            }
            else
            {
                //guncelle
                Kitap? kitapVT = _kitapRepository.Get(u => u.Id == id); //public T Get(Expression<Func<T, bool>> filtre) Repositorunin içine bak
                if (kitapVT == null)
                {
                    return NotFound();
                }
                return View(kitapVT);
            }

        }
        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            //modelstatee bağlı hataları bulmak için kullanıyoruz
            //var errors = ModelState.Values.SelectMany(x => x.Errors);

            //bu backend ile validation yazdırma HATA MESAJLARI
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");
                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }

                if (kitap.Id == 0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Oluşturuldu!";

                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = "Yeni Kitap Başarıyla Güncellendi!";

                }

                _kitapRepository.Kaydet();
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        /* KODLARI TEMİZ HALE ÇEKİYORUZ EKLE KOMUTUNUN İÇERİSİNDEGÜNCELLEME KOMUTUDA MEVCUT
        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVT = _kitapRepository.Get(u=>u.Id==id); //public T Get(Expression<Func<T, bool>> filtre) Repositorunin içine bak
            if (kitapVT == null)
            {
                return NotFound();
            }
            return View(kitapVT);
        }
        */
        /*
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();
                TempData["basarili"] = "Kitap Başarıyla Güncellendi!";
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        */
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVT = _kitapRepository.Get(u => u.Id == id);
            if (kitapVT == null)
            {
                return NotFound();
            }
            return View(kitapVT);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int id)
        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kitap Başarıyla Silindi!";
            return RedirectToAction("Index", "Kitap");
        }
    }
}




