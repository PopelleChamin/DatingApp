namespace API.UnitTests.Tests;

using System.Net;
using System.Text;
using API.DTOs;
using API.UnitTests.Helpers;
using Newtonsoft.Json.Linq;

public class AccountControllerTests
{
    private readonly HttpClient _client;

    public AccountControllerTests()
    {
        _client = TestHelper.Instance.Client;
    }

    [Fact]
    public async Task GetSecretShouldReturnUnauthorizedWhenUserDoesNotExist()
    {
        // Arrange
        var expectedStatusCode = "Unauthorized"; // Aquí esperamos un código de estado de autorización fallida
        var requestUrl = "api/account/login";
        var loginRequest = new LoginRequest
        {
            Username = "arenita03", // Usuario que no existe
            Password = "123456"
        };

        var loginObject = GetLoginObject(loginRequest);
        var httpContent = GetHttpContent(loginObject);

        // Act
        var httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(expectedStatusCode, true), httpResponse.StatusCode);
        Assert.Equal(expectedStatusCode, httpResponse.StatusCode.ToString());
    }

    [Fact]
    public async Task GetSecretShouldReturnUnauthorizedWhenPasswordIsInvalid()
    {
        // Arrange
        var expectedStatusCode = "Unauthorized"; // Aquí esperamos un código de estado de autorización fallida
        var requestUrl = "api/account/login";
        var loginRequest = new LoginRequest
        {
            Username = "arenita", // Usuario que no existe
            Password = "1234567"
        };

        var loginObject = GetLoginObject(loginRequest);
        var httpContent = GetHttpContent(loginObject);

        // Act
        var httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(expectedStatusCode, true), httpResponse.StatusCode);
        Assert.Equal(expectedStatusCode, httpResponse.StatusCode.ToString());
    }

    [Fact]
    public async Task GetSecretShouldOkRegister()
    {
        // Arrange
        var expectedStatusCode = "OK"; // Aquí esperamos un código de estado de autorización fallida
        var requestUrl = "api/account/register";
        var registerRequest = new RegisteRequest
        {
            Username = "arenita2", // Usuario que no existe
            Password = "1234567"
        };

        var registerObject = GetRegisterObject(registerRequest);
        var httpContent = GetHttpContent(registerObject);

        // Act
        var httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(expectedStatusCode, true), httpResponse.StatusCode);
        Assert.Equal(expectedStatusCode, httpResponse.StatusCode.ToString());
    }

    [Fact]
    public async Task GetSecretShouldBadRequestRegister()
    {
        // Arrange
        var expectedStatusCode = "BadRequest"; // Aquí esperamos un código de estado de autorización fallida
        var requestUrl = "api/account/register";
        var registerRequest = new RegisteRequest
        {
            Username = "arenita", // Usuario que ya existe
            Password = "1234567"
        };

        var registerObject = GetRegisterObject(registerRequest);
        var httpContent = GetHttpContent(registerObject);

        // Act
        var httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var responseContent = await httpResponse.Content.ReadAsStringAsync();

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(expectedStatusCode, true), httpResponse.StatusCode);
        Assert.Equal(expectedStatusCode, httpResponse.StatusCode.ToString());
    }

    #region Privated methods

    private static string GetLoginObject(LoginRequest loginDto)
    {
        var entityObject = new JObject()
            {
                { nameof(loginDto.Username), loginDto.Username },
                { nameof(loginDto.Password), loginDto.Password }
            };

        return entityObject.ToString();
    }

    private static string GetRegisterObject(RegisteRequest registerDto)
    {
        var entityObject = new JObject()
            {
                { nameof(registerDto.Username), registerDto.Username },
                { nameof(registerDto.Password), registerDto.Password }
            };

        return entityObject.ToString();
    }

    private static StringContent GetHttpContent(string objectToCode) =>
        new(objectToCode, Encoding.UTF8, "application/json");

    #endregion
}