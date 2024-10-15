using System.ComponentModel.DataAnnotations;
using Portail_OptiVille.Data.Attributes;

namespace Portail_OptiVille.Data.FormModels
{
    public class CoordonneeFormModel
    {
        public CoordonneeFormModel() {
            PhoneList = new List<TelephoneFormModel>();
        }

        [Required(ErrorMessage = "\u2116 requis")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,8}$", ErrorMessage = "Entre 1 à 8 chiffres ou lettres.")]
        public string NoEntreprise { get; set; }

        [Required(ErrorMessage = "Rue requise")]
        public string RueEntreprise { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{1,8}$", ErrorMessage = "Entre 1 à 8 chiffres ou lettres.")]
        public string? BureauEntreprise { get; set; }

        [Ville]
        public string VilleEntreprise { get; set; }

        [Required(ErrorMessage = "Province requise")]
        public string ProvinceEntreprise { get; set; }

        [Required(ErrorMessage = "CP requis")]
        [RegularExpression(@"^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$", ErrorMessage = "Format invalide")]
        public string CodePostalEntreprise { get; set; }

        [Required(ErrorMessage = "Région adm. requise")]
        public string? RegionAdmEntreprise { get; set; }

        public string? SiteWebEntreprise { get; set; }

        public List<TelephoneFormModel>? PhoneList { get ; set; }

        public string FullAddressPart1
        {
            get
            {
                return $"{NoEntreprise} {RueEntreprise} {BureauEntreprise}";
            }
            set
            {
                var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0) NoEntreprise = parts[0];
                if (parts.Length > 1) RueEntreprise = parts[1];
                if (parts.Length > 2) BureauEntreprise = parts[2];
            }
        }

        public string FullAddressPart2
        {
            get
            {
                return $"{VilleEntreprise} {ProvinceEntreprise} {CodePostalEntreprise}";
            }
            set
            {
                var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0) VilleEntreprise = parts[0];
                if (parts.Length > 1) ProvinceEntreprise = parts[1];
                if (parts.Length > 2) CodePostalEntreprise = parts[2];
            }
        }
    }
}
