using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Portail_OptiVille.Data.Attributes
{
    public class NEQRegex : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var stringValue = value as string;
            if (stringValue != null)
            {
                if (stringValue.Length < 2 || !Regex.IsMatch(stringValue.Substring(0, 2), @"^(11|22|33|88)"))
                {
                    return new ValidationResult("Doit commencer par 11, 22, 33 ou 88", new[] { validationContext.MemberName });
                }
                if (stringValue.Length < 3 || !"456789".Contains(stringValue[2]))
                {
                    return new ValidationResult("Troisième caractère: 4, 5, 6, 7, 8 ou 9", new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }
    }
}
