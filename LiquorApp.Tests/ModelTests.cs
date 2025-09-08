using FluentAssertions;
using LiquorApp.Api.Data;
using LiquorApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LiquorApp.Tests;

public class ModelTests
{
    [Fact]
    public void User_Email_HasUniqueIndex()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        using var db = new AppDbContext(options);
        var entity = db.Model.FindEntityType(typeof(User));
        entity.Should().NotBeNull();
        var hasUniqueIndex = entity!.GetIndexes().Any(i => i.IsUnique && i.Properties.Any(p => p.Name == nameof(User.Email)));
        hasUniqueIndex.Should().BeTrue();
    }
}
