using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class MDPRegexIdentification : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var password = value as string;
        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Mot de passe requis", new[] { validationContext.MemberName });
        }
        if (password.Length < 7 || password.Length > 12 
            || !Regex.IsMatch(password, @"[A-Z]")
            || !Regex.IsMatch(password, @"[a-z]")
            || !Regex.IsMatch(password, @"[0-9]")
            || !Regex.IsMatch(password, @"[!@#$%^&*(),.?""':;{}|<>]"))
        {
            var message = @"
                <div style=""text-align: left;"">
                    Doit contenir : <br>
                    - Entre 7 à 12 caractères<br>
                    - Une majuscule<br>
                    - Une minuscule<br>
                    - Un chiffre<br>
                    - Un caractère spécial
                </div>";
        
            return new ValidationResult(message, new[] { validationContext.MemberName });
        }
        return ValidationResult.Success;
    }
}

