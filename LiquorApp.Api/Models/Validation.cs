using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LiquorApp.Api.Models;

public static class Validation
{
    public static ModelStateDictionary ToModelState(this IEnumerable<ValidationResult> results)
    {
        var ms = new ModelStateDictionary();
        foreach (var r in results)
        {
            var key = r.MemberNames?.FirstOrDefault() ?? string.Empty;
            ms.AddModelError(key, r.ErrorMessage ?? "Invalid value");
        }
        return ms;
    }

    public static Dictionary<string, string[]> ValidateRegisterRequest(RegisterUserRequest req)
    {
        var errors = new Dictionary<string, string[]>();

        void Add(string key, string message)
        {
            if (!errors.TryGetValue(key, out var list))
            {
                errors[key] = new[] { message };
            }
            else
            {
                errors[key] = list.Concat(new[] { message }).ToArray();
            }
        }

        if (string.IsNullOrWhiteSpace(req.FirstName)) Add(nameof(req.FirstName), "First name is required");
        if (string.IsNullOrWhiteSpace(req.LastName)) Add(nameof(req.LastName), "Last name is required");

        if (string.IsNullOrWhiteSpace(req.Email)) Add(nameof(req.Email), "Email is required");
        else if (!new EmailAddressAttribute().IsValid(req.Email)) Add(nameof(req.Email), "Invalid email format");

        if (string.IsNullOrWhiteSpace(req.Password)) Add(nameof(req.Password), "Password is required");
        else
        {
            if (req.Password.Length < 8) Add(nameof(req.Password), "Password must be at least 8 characters");
            if (!req.Password.Any(char.IsUpper)) Add(nameof(req.Password), "Password must contain an uppercase letter");
            if (!req.Password.Any(char.IsLower)) Add(nameof(req.Password), "Password must contain a lowercase letter");
            if (!req.Password.Any(char.IsDigit)) Add(nameof(req.Password), "Password must contain a digit");
            if (!req.Password.Any(ch => !char.IsLetterOrDigit(ch))) Add(nameof(req.Password), "Password must contain a symbol");
        }

        if (string.IsNullOrWhiteSpace(req.ConfirmPassword)) Add(nameof(req.ConfirmPassword), "Confirm password is required");
        else if (!string.Equals(req.Password, req.ConfirmPassword)) Add(nameof(req.ConfirmPassword), "Passwords do not match");

        return errors;
    }
}