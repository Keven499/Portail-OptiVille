using System.ComponentModel.DataAnnotations;
using Portail_OptiVille.Data.Attributes;
using System.Text.RegularExpressions;

namespace Portail_OptiVille.Data.FormModels
{
    public class CoordonneeFormModel
    {

        public int IdCoordonnee { get; set; }
        public CoordonneeFormModel() {
            PhoneList = new List<TelephoneFormModel>();
        }

        [Required(ErrorMessage = "\u2116 requis")]
        [RegularExpression(@"^[a-zA-Z0-9/]{1,8}$", ErrorMessage = "Entre 1 à 8 chiffres ou lettres.")]
        public string NoEntreprise { get; set; }

        [Required(ErrorMessage = "Rue requise")]
        public string RueEntreprise { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{1,8}$", ErrorMessage = "Entre 1 à 8 chiffres ou lettres.")]
        public string? BureauEntreprise { get; set; }

        [Required(ErrorMessage = "Ville requise")]
        public string VilleEntreprise { get; set; }

        // [Required(ErrorMessage = "Province requise")]
        public string ProvinceEntreprise { get; set; }

        [Required(ErrorMessage = "CP requis")]
        [RegularExpression(@"^[A-Za-z][0-9][A-Za-z] ?[0-9][A-Za-z][0-9]$", ErrorMessage = "Format invalide")]
        public string CodePostalEntreprise
        {
            get => _codePostalEntreprise;
            set => _codePostalEntreprise = NormalizeCodePostal(value);
        }

        [Required(ErrorMessage = "Région adm. requise")]
        public string? RegionAdmEntreprise { get; set; }

        public string? CodeRegionAdmEntreprise { get; set; }

        public string? SiteWebEntreprise { get; set; }

        public List<TelephoneFormModel>? PhoneList { get ; set; }

        private string _codePostalEntreprise;
        public string NormalizeCodePostal(string codePostal)
        {
            if (string.IsNullOrWhiteSpace(codePostal))
                return codePostal;

            // Enlève les espaces et met tout en majuscules
            return Regex.Replace(codePostal.ToUpper(), @"\s+", "");
        }
    }    
}
