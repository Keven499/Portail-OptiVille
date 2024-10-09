using System.ComponentModel.DataAnnotations;
using Mysqlx;

namespace Portail_OptiVille.Data.FormModels
{
    public class FournisseurFormModel
    {
        public FournisseurFormModel() {
            PhoneList = new List<TelephoneFormModel>();
        }

        [Required(ErrorMessage = "Requis")]
        [RegularExpression(@"^(\d{1,5}[A-Z]?)\s*(½?)$", ErrorMessage = "Format invalide")]
        public string NoEntreprise { get; set; }

        [Required(ErrorMessage = "Requise")]
        public string RueEntreprise { get; set; }

        public string? BureauEntreprise { get; set; }

        [Required(ErrorMessage = "Requise")]
        public string VilleEntreprise { get; set; }

        [Required(ErrorMessage = "Requise")]
        public string ProvinceEntreprise { get; set; }

        [Required(ErrorMessage = "Requis")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "6 caractères max")]
        [RegularExpression(@"^[A-z]{1}[\d]{1}[A-z]{1}[\d]{1}[A-z]{1}[\d]{1}$", ErrorMessage = "Format invalide")]
        public string CodePostalEntreprise { get; set; }

        [Required(ErrorMessage = "Requise")]
        public string? RegionAdmEntreprise { get; set; }

        public string? SiteWebEntreprise { get; set; }

        public List<TelephoneFormModel>? PhoneList { get ; set; }
    }
}
