using System.ComponentModel.DataAnnotations;
using Portail_OptiVille.Data.Attributes;

namespace Portail_OptiVille.Data.FormModels
{
    public class IdenticationFormModel
    {
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 caractères exigés")]
        [NEQRegex]
        public string? NEQ { get; set; }

        [Required(ErrorMessage = "Nom requis")]
        public string? NomEntreprise { get; set; }

        [Required(ErrorMessage = "Courriel requis")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Format invalide")]
        public string? CourrielEntreprise { get; set; }

        [MDPRegexIdentification]
        public string? MotDePasse { get; set; }

        [Required(ErrorMessage = "Confirmation requise")]
        [MatchField("MotDePasse", ErrorMessage = "Ne correspondent pas")]
        public string? ConfirmationMotDePasse { get; set; }
    }
}
