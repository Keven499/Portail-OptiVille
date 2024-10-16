using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Portail_OptiVille.Data.Attributes
{
    public class VilleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult("Ville requise", new[] { validationContext.MemberName });
            }

            string stringValue = value.ToString();
            if (stringValue.StartsWith("-") || stringValue.EndsWith("-"))
            {
                return new ValidationResult("Pas de - en début et fin", new[] { validationContext.MemberName });
            }

            var regex = new Regex(@"^[a-zA-ZÀ-ÿ\-]+$");
            if (!regex.IsMatch(stringValue))
            {
                return new ValidationResult("Que des lettres et tirets", new[] { validationContext.MemberName });
            }

            if (Regex.IsMatch(stringValue, @"[^a-zA-ZÀ-ÿ\-]"))
            {
                return new ValidationResult("Caractères interdits", new[] { validationContext.MemberName });
            }


            return ValidationResult.Success;
        }
    }
}
