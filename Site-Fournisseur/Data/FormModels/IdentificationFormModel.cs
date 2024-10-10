using System.ComponentModel.DataAnnotations;
using Portail_OptiVille.Data.Attributes;

namespace Portail_OptiVille.Data.FormModels
{
    public class IdenticationFormModel
    {
        [StringLength(10, MinimumLength = 10, ErrorMessage = "10 caractères exigés")]
        public string? NEQ { get; set; }

        [Required(ErrorMessage = "Requis")]
        public string? NomEntreprise { get; set; }

        [Required(ErrorMessage = "Requis")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Format invalide")]
        public string? CourrielEntreprise { get; set; }

        [Required(ErrorMessage = "Requis")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "7 à 12 caractères")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&]).+$", ErrorMessage = "Caractères interdits")]
        // IL MANQUE DES VALIDATIONS POUR LE MOT DE PASSE
        public string? MotDePasse { get; set; }

        [Required(ErrorMessage = "Requis")]
        [MatchField("MotDePasse", ErrorMessage = "Ne correspondent pas")]
        public string? ConfirmationMotDePasse { get; set; }
    }
}
