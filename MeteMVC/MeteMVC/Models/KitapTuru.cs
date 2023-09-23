using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MeteMVC.Models
{
    public class KitapTuru
    {
        [Key] //PKEY
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap Tür Adı boş bırakılamaz!!!")] //NOT NULL
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı")]
        public string Ad { get; set; }
    }
}

