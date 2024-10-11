using System;
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
                return new ValidationResult("Ville requise");
            }

            string stringValue = value.ToString();
            if (stringValue.StartsWith("-") || stringValue.EndsWith("-"))
            {
                return new ValidationResult("Pas de - en début et fin");
            }

            var regex = new Regex(@"^[a-zA-Z-]+$");
            if (!regex.IsMatch(stringValue))
            {
                return new ValidationResult("Que des lettres et tirets");
            }

            if (Regex.IsMatch(stringValue, @"[^a-zA-Z-]"))
            {
                return new ValidationResult("Caractères interdits");
            }

            return ValidationResult.Success;
        }
    }
}
