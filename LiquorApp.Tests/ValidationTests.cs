using FluentAssertions;
using LiquorApp.Api.Models;
using Xunit;

namespace LiquorApp.Tests;

public class ValidationTests
{
    [Fact]
    public void InvalidPassword_ShouldReturnErrors()
    {
        var req = new RegisterUserRequest
        {
            FirstName = "A",
            LastName = "B",
            Email = "user@example.com",
            Password = "short",
            ConfirmPassword = "short"
        };

        var errors = Validation.ValidateRegisterRequest(req);
        errors.Should().ContainKey(nameof(RegisterUserRequest.Password));
    }

    [Fact]
    public void MismatchedPassword_ShouldReturnError()
    {
        var req = new RegisterUserRequest
        {
            FirstName = "A",
            LastName = "B",
            Email = "user@example.com",
            Password = "Abcdef1$",
            ConfirmPassword = "Abcdef1%"
        };

        var errors = Validation.ValidateRegisterRequest(req);
        errors.Should().ContainKey(nameof(RegisterUserRequest.ConfirmPassword));
    }
}
