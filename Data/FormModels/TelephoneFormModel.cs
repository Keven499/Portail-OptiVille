using System.ComponentModel.DataAnnotations;

namespace Portail_OptiVille.Data.FormModels {
    public class TelephoneFormModel
    {
        public int IdTelephone { get; set; }

        [Required(ErrorMessage = "Type requis")]
        public string TypeTelEntreprise { get; set; }
    
        [Required(ErrorMessage = "No requis")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 caractères exigés")]
        public string NoTelEntreprise { get; set; }

        [StringLength(6, MinimumLength = 6, ErrorMessage = "6 caractères exigés")]
        public string? PosteTelEntreprise { get; set; }
    }
}