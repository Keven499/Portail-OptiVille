using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class MDPRegexIdentification : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        Console.WriteLine($"REGEX VALIDATION FOR MDP");
        var password = value as string;
        if (string.IsNullOrEmpty(password))
        {
            return new ValidationResult("Mdp requis", new[] { validationContext.MemberName });
        }

        Console.WriteLine($"Password is not null and its value is: {password}");
        if (password.Length < 7 || password.Length > 12)
        {
            return new ValidationResult("7 à 12 caractères", new[] { validationContext.MemberName });
        }
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return new ValidationResult("Doit contenir une majuscule", new[] { validationContext.MemberName });
        }
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return new ValidationResult("Doit contenir une minuscule", new[] { validationContext.MemberName });
        }
        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            return new ValidationResult("Doit contenir un chiffre", new[] { validationContext.MemberName });
        }
        if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':;{}|<>]"))
        {
            return new ValidationResult("Doit contenir un caractère spécial", new[] { validationContext.MemberName });
        }

        return ValidationResult.Success;
    }

}
