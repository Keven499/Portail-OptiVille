using System.ComponentModel.DataAnnotations;

namespace Portail_OptiVille.Data.FormModels {
    public class ContactFormModel
    {
        public int IdContact { get; set; }

        [Required(ErrorMessage = "Prénom requis")]
        [RegularExpression("^[a-zA-Z'-]+$", ErrorMessage = "Caractères interdits")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Nom requis")]
        [RegularExpression("^[a-zA-Z'-]+$", ErrorMessage = "Caractères interdits")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Fonction requise")]      
        public string Fonction { get; set; }

        [Required(ErrorMessage = "Courriel requis")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Caractères interdits")]
        public string AdresseCourriel { get; set; }

        [Required(ErrorMessage = "Type requis")]
        public string TypeTelephone { get; set; } = null!;

        [Required(ErrorMessage = "No requis")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Caractères interdits")]
        public string Telephone { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "6 caractères exigés")]
        public string? Poste { get; set; }
    }
}