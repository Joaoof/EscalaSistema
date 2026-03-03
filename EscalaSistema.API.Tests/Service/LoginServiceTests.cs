using System.Security.Claims;
using DevOne.Security.Cryptography.BCrypt;
using EscalaSistema.API.Domain.Errors;
using EscalaSistema.API.DTOs.Login;
using EscalaSistema.API.Enum;
using EscalaSistema.API.Interface.Repository;
using EscalaSistema.API.Interface.UseCase;
using EscalaSistema.API.Models;
using EscalaSistema.API.Service;
using FluentAssertions;
using Moq;

namespace EscalaSistema.API.Tests.Service;

public class LoginServiceTests
{
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<ILoginRepository> _loginRepositoryMock;
    private readonly LoginService _sut;

    public LoginServiceTests()
    {
        _tokenServiceMock = new Mock<ITokenService>();
        _loginRepositoryMock = new Mock<ILoginRepository>();
        _sut = new LoginService(_tokenServiceMock.Object, _loginRepositoryMock.Object);
    }

    [Fact]
    public async Task AuthenticateAsync_WhenCredentialsAreValid_ShouldReturnLoginResponseWithToken()
    {
        // Arrange
        var password = "Strong#123";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "jdoe",
            Email = "john@escala.com",
            PasswordHash = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()),
            Role = UserRoleEnum.Leader,
            IsActive = true
        };

        var request = new LoginRequest
        {
            Email = user.Email,
            PasswordHash = password
        };

        _loginRepositoryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        _tokenServiceMock
            .Setup(x => x.GenerateToken(user))
            .Returns("jwt-token");

        // Act
        var response = await _sut.AuthenticateAsync(request);

        // Assert
        response.UserId.Should().Be(user.Id);
        response.Username.Should().Be(user.Username);
        response.Token.Should().Be("jwt-token");

        _loginRepositoryMock.Verify(x => x.GetByEmailAsync(request.Email), Times.Once);
        _tokenServiceMock.Verify(x => x.GenerateToken(user), Times.Once);
    }

    [Fact]
    public async Task AuthenticateAsync_WhenUserDoesNotExist_ShouldThrowDomainException()
    {
        // Arrange
        var request = new LoginRequest
        {
            Email = "missing@escala.com",
            PasswordHash = "Any#1234"
        };

        _loginRepositoryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _sut.AuthenticateAsync(request);

        // Assert
        var exception = await act.Should().ThrowAsync<DomainException>();
        exception.Which.Error.Should().BeSameAs(AuthErrors.NotValid);
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task AuthenticateAsync_WhenPasswordIsInvalid_ShouldThrowDomainException()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "jdoe",
            Email = "john@escala.com",
            PasswordHash = BCryptHelper.HashPassword("Correct#123", BCryptHelper.GenerateSalt()),
            IsActive = true
        };

        var request = new LoginRequest
        {
            Email = user.Email,
            PasswordHash = "Wrong#123"
        };

        _loginRepositoryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _sut.AuthenticateAsync(request);

        // Assert
        var exception = await act.Should().ThrowAsync<DomainException>();
        exception.Which.Error.Should().BeSameAs(AuthErrors.NotValid);
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task AuthenticateAsync_WhenUserIsInactive_ShouldThrowDomainException()
    {
        // Arrange
        var password = "Strong#123";
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "jdoe",
            Email = "john@escala.com",
            PasswordHash = BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt()),
            IsActive = false
        };

        var request = new LoginRequest
        {
            Email = user.Email,
            PasswordHash = password
        };

        _loginRepositoryMock
            .Setup(x => x.GetByEmailAsync(request.Email))
            .ReturnsAsync(user);

        // Act
        Func<Task> act = async () => await _sut.AuthenticateAsync(request);

        // Assert
        var exception = await act.Should().ThrowAsync<DomainException>();
        exception.Which.Error.Should().BeSameAs(AuthErrors.NotValid);
        _tokenServiceMock.Verify(x => x.GenerateToken(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    public async Task GetCurrentUserInfo_WhenClaimsArePresent_ShouldMapIdAndUsername()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, "jdoe"),
                new Claim(ClaimTypes.Role, UserRoleEnum.Leader.ToString())
            ],
            authenticationType: "TestAuth"
        ));

        // Act
        var result = await _sut.GetCurrentUserInfo(principal);

        // Assert
        result.Id.Should().Be(userId);
        result.Username.Should().Be("jdoe");
        result.Role.Should().Be(UserRoleEnum.Musician);
    }

    [Fact]
    public async Task GetCurrentUserInfo_WhenNameIdentifierIsMissing_ShouldReturnEmptyGuid()
    {
        // Arrange
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(ClaimTypes.Name, "jdoe")],
            authenticationType: "TestAuth"
        ));

        // Act
        var result = await _sut.GetCurrentUserInfo(principal);

        // Assert
        result.Id.Should().Be(Guid.Empty);
        result.Username.Should().Be("jdoe");
        result.Role.Should().Be(UserRoleEnum.Musician);
    }

    [Fact]
    public async Task GetCurrentUserInfo_WhenNameIdentifierIsNotGuid_ShouldReturnEmptyGuid()
    {
        // Arrange
        var principal = new ClaimsPrincipal(new ClaimsIdentity(
            [new Claim(ClaimTypes.NameIdentifier, "not-a-guid")],
            authenticationType: "TestAuth"
        ));

        // Act
        var result = await _sut.GetCurrentUserInfo(principal);

        // Assert
        result.Id.Should().Be(Guid.Empty);
        result.Username.Should().BeNull();
        result.Role.Should().Be(UserRoleEnum.Musician);
    }
}
