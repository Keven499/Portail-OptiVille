using System.ComponentModel.DataAnnotations;
using Mysqlx;

namespace Portail_OptiVille.Data.FormModels
{
    public class FournisseurFormModel
    {
        public FournisseurFormModel() {
            PhoneList = new List<TelephoneFormModel>();
        }

        [Required(ErrorMessage = "No requis")]
        [RegularExpression(@"^[a-zA-Z0-9]{1,8}$", ErrorMessage = "Entre 1 à 8 chiffres ou lettres.")]
        public string NoEntreprise { get; set; }

        [Required(ErrorMessage = "Rue requise")]
        public string RueEntreprise { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]{1,8}$", ErrorMessage = "Entre 1 à 8 chiffres ou lettres.")]
        public string? BureauEntreprise { get; set; }

        [Required(ErrorMessage = "Ville requise")]
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
    }
}
