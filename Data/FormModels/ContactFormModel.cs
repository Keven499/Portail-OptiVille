using System.ComponentModel.DataAnnotations;
using Portail_OptiVille.Data.Attributes;
using System.Text.RegularExpressions;

namespace Portail_OptiVille.Data.FormModels {
    public class ContactFormModel
    {
        public int IdContact { get; set; }

        [Required(ErrorMessage = "Prénom requis")]
        [RegularExpression(@"^[a-zA-Z'\-]{1,32}$", ErrorMessage = "Doit contenir entre 1 et 32 caractères et ' ou -")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Nom requis")]
        [RegularExpression(@"^[a-zA-Z'\-]{1,32}$", ErrorMessage = "Peut contenir seulement ' ou -")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Fonction requise")]
        [StringLength(32, ErrorMessage = "La fonction ne peut pas dépasser 32 caractères.")]
        [RegularExpression(@"^[^0-9]*$", ErrorMessage = "Ne peut contenir des chiffres.")]
        public string Fonction { get; set; }

        [Required(ErrorMessage = "Courriel requis")]
        [RegularExpression(@"^[\w\-\.]{1,64}@([\w-]+\.)+[\w-]{2,}$", ErrorMessage = "Format invalide")]
        public string AdresseCourriel { get; set; }

        [Required(ErrorMessage = "Type requis")]
        public string TypeTelephone { get; set; } = null!;

        [Required(ErrorMessage = "No requis")]
        [RegularExpression(@"^(\(?\d{3}\)?[-.\s]?)?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Format invalide")]
        public string Telephone
        {
            get => _Telephone;
            set => _Telephone = NormalizePhoneNumber(value);
        }

        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Chiffres uniquement")]
        public string? Poste { get; set; }

        private string _Telephone;
        public string NormalizePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            // Enlève tout sauf les chiffres
            return Regex.Replace(phoneNumber, @"\D", "");
        }
    }
}