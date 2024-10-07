using System.ComponentModel.DataAnnotations;

namespace Portail_OptiVille.Data.FormModels {
    public class TelephoneFormModel
    {
        public int IdTelephone { get; set; }

        [Required(ErrorMessage = "Type requis")]
        public string TypeTelEntreprise { get; set; }
    
        [Required(ErrorMessage = "Numéro requis")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Doit contenir 10 caractères")]
        public string NoTelEntreprise { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "Doit contenir 6 caractères")]
        public string? PosteTelEntreprise { get; set; }
    }
}