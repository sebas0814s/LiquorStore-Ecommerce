using FluentAssertions;
using LiquorApp.Api.Services;
using Xunit;

namespace LiquorApp.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void HashAndVerify_Works()
    {
        var hasher = new PasswordHasher();
        var (hash, salt) = hasher.HashPassword("Abcdef1$");
        hasher.Verify("Abcdef1$", hash, salt).Should().BeTrue();
        hasher.Verify("Wrong1$", hash, salt).Should().BeFalse();
    }
}
