using System.ComponentModel.DataAnnotations;

namespace iPhone.Models
{
    public class CreateViewTelefon
    {
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Model Adı Yazınız")]
		[Display(Name = "Model Adı")]
		public string? Model { get; set; }

		[Required(ErrorMessage = "Ekran Boyut Yazınız")]
		[Display(Name = "Ekran ")]
		public string? Ekran { get; set; }

		[Required(ErrorMessage = "İşlemci  Yazınız")]
		[Display(Name = "İşlemci")]
		public string? İşlemci { get; set; }

		[Required(ErrorMessage = "Kamera  Yazınız")]
		[Display(Name = "Kamera ")]
		public string? Kamera { get; set; }

		[Required(ErrorMessage = "Kurs Renk  Yazınız")]
		[Display(Name = "Renk ")]
		public string? Renk { get; set; }

		[Required(ErrorMessage = "Kurs Fiyatı Yazınız")]
		[Display(Name = "Fiyatı")]
		public float? Fiyatı { get; set; }

		[Required(ErrorMessage = "Ürün Resmini giriniz")]
		public IFormFile Photo { get; set; }
		public string? PhotoFileName { get; set; } //photopathaynı görevi görüyor
    }
}
