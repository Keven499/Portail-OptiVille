using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Portail_OptiVille.Data.FormModels {
    public class TelephoneFormModel
    {
        public int IdTelephone { get; set; }

        [Required(ErrorMessage = "Type requis")]
        public string TypeTelEntreprise { get; set; }
    
        [Required(ErrorMessage = "No requis")]
        [RegularExpression(@"^(\(?\d{3}\)?[-.\s]?)?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Format invalide")]
        public string NoTelEntreprise
        {
            get => _noTelEntreprise;
            set => _noTelEntreprise = NormalizePhoneNumber(value);
        }

        [RegularExpression(@"^\d{1,6}$", ErrorMessage = "Chiffres uniquement")]
        public string? PosteTelEntreprise { get; set; }

        private string _noTelEntreprise;
        public string NormalizePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return phoneNumber;

            // Enlève tout sauf les chiffres
            return Regex.Replace(phoneNumber, @"\D", "");
        }

    }
}