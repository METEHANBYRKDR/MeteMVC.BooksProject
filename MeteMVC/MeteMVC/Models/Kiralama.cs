using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeteMVC.Models
{
    public class Kiralama
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OgrenciId { get; set; }


        [ValidateNever]
        [ForeignKey("KitapId")]
        public int KitapId { get; set; }

        [ValidateNever]
        public Kitap Kitap { get; set; }

    }
}
