using Application.Abstractions.Identity;
using Application.Common.Results;
using Application.Members.Inputs;
using Application.Members.Services;
using Domain.Abstractions.Repositories.Members;
using Domain.Aggregates.Members;
using Infrastructure.Persistence;
using Infrastructure.Persistence.EfCore.Contexts;
using Infrastructure.Persistence.EfCore.Repositories.Members;
using Infrastructure.Persistence.EfCore.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

public class RegisterMemberServiceIntegrationTests
{
    [Fact]
    public async Task ExecuteAsync_ShouldCreateMemberInDatabase_WhenUserIsCreatedSuccessfully()
    {
        // Arrange – skapa en riktig in-memory SQLite-databas
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlite(connection)
            .Options;

        using var context = new DataContext(options);
        context.Database.EnsureCreated(); // Skapar tabellerna

        // Riktig repository
        IMemberRepository memberRepository = new MemberRepository(context);

        // Mockad IdentityService
        var identityMock = new Mock<IIdentityService>();
        identityMock
            .Setup(x => x.CreateUserAsync("test@test.com", "Password123", default))
            .ReturnsAsync(Result<string?>.Ok("user-123"));

        var service = new RegisterMemberService(identityMock.Object, memberRepository);

        var input = new RegisterMemberInput("test@test.com", "Password123");

        // Act – kör hela flödet
        var result = await service.ExecuteAsync(input);

        // Assert – kontrollera resultat
        Assert.True(result.Success);
        Assert.Equal("user-123", result.Value);

        // Assert – kontrollera att medlemmen faktiskt sparats i databasen
        var savedMember = await context.Members.FirstOrDefaultAsync(m => m.UserId == "user-123");
        Assert.NotNull(savedMember);
        Assert.Equal("user-123", savedMember.UserId);
    }
}
