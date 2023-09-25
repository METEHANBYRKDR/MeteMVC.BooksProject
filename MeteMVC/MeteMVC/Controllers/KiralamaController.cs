using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MeteMVC.Models;
using MeteMVC.Utility;
using Microsoft.AspNetCore.Authorization;

namespace MeteMVC.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]

    public class KiralamaController : Controller
    {

        private readonly IKiralamaRepository _kiralamaRepository;   //program.cs nin içindeki service kodundan kaynaklı newlememize gerek yok .net bizim için konteynırda bu elemanı tutuyor DEPENDENCY INJECTION soli(D)
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;



        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();  //SQL İN İÇERSİNDEKİ VERİLERİ ÇEKTİ MVC NİN MODEL KISMI !!!1değişiklik include metoduyla kitap daki foreign keyleri getirdik KitapTuru,KitapTuruId  
            return View(objKiralamaList);   // Viewe göndermek için yaptık


        }
        public IActionResult EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });
            ViewBag.KitapList = KitapList;
            if (id == null || id == 0)
            {
                //ekle
                return View();
            }
            else
            {
                //guncelle
                Kiralama? kiralamaVT = _kiralamaRepository.Get(u => u.Id == id); //public T Get(Expression<Func<T, bool>> filtre) Repositorunin içine bak
                if (kiralamaVT == null)
                {
                    return NotFound();
                }
                return View(kiralamaVT);
            }

        }
        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            //modelstatee bağlı hataları bulmak için kullanıyoruz
            //var errors = ModelState.Values.SelectMany(x => x.Errors);

            //bu backend ile validation yazdırma HATA MESAJLARI
            if (ModelState.IsValid)
            {
               
                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Ekle(kiralama);
                    TempData["basarili"] = "Yeni Kiralama Başarıyla Oluşturuldu!";

                }
                else
                {
                    _kiralamaRepository.Guncelle(kiralama);
                    TempData["basarili"] = "Yeni Kiralama Başarıyla Güncellendi!";

                }

                _kiralamaRepository.Kaydet();
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll().Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });
            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kiralama? kiralamaVT = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralamaVT == null)
            {
                return NotFound();
            }
            return View(kiralamaVT);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }
            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kiralama Başarıyla Silindi!";
            return RedirectToAction("Index", "Kiralama");
        }
    }
}




