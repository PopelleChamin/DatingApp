namespace DatingApp.UnitTests.Test;

using API.UnitTests.Helpers;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using API.DTOs;

public class UsersControllerTests
{
    private string apiRoute = "api/users";
    private readonly HttpClient _client;
    private HttpResponseMessage httpResponse;
    private string requestUrl;
    private string loginObjetct;
    private string memberObjetct;
    private HttpContent httpContent;

    public UsersControllerTests()
    {
        _client = TestHelper.Instance.Client;
    }

    [Theory]
    [InlineData("OK", "bob", "123456")]
    public async Task GetUsersShouldOK(string statusCode, string username, string password)
    {
        // Arrange
        requestUrl = "api/account/login";
        var loginDto = new LoginRequest
        {
            Username = username,
            Password = password
        };

        loginObjetct = GetLoginObject(loginDto);
        httpContent = GetHttpContent(loginObjetct);

        httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var reponse = await httpResponse.Content.ReadAsStringAsync();
        var userResponse = JsonSerializer.Deserialize<UserResponse>(reponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userResponse.Token);
        
        requestUrl = $"{apiRoute}";

        // Act
        httpResponse = await _client.GetAsync(requestUrl);

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
        Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
    }

    [Theory]
    [InlineData("OK", "bob", "123456")]
    public async Task GetUserByUsernameShouldOK(string statusCode, string username, string password)
    {
        // Arrange
        requestUrl = "api/account/login";
        var loginDto = new LoginRequest
        {
            Username = username,
            Password = password
        };

        loginObjetct = GetLoginObject(loginDto);
        httpContent = GetHttpContent(loginObjetct);

        httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var reponse = await httpResponse.Content.ReadAsStringAsync();
        var userResponse = JsonSerializer.Deserialize<UserResponse>(reponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userResponse.Token);

        requestUrl = $"{apiRoute}/" + username;

        // Act
        httpResponse = await _client.GetAsync(requestUrl);

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
        Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
    }

    [Theory]
    [InlineData("NoContent", "bob", "123456", "IntroductionU", "LookingForU", "InterestsU", "CityU", "CountryU")]
    public async Task UpdateUserShouldNoContent(string statusCode, string username, string password, string introduction, string lookingFor, string interests, string city, string country)
    {
        // Arrange
        requestUrl = "api/account/login";
        var loginDto = new LoginRequest
        {
            Username = username,
            Password = password
        };

        loginObjetct = GetLoginObject(loginDto);
        httpContent = GetHttpContent(loginObjetct);

        httpResponse = await _client.PostAsync(requestUrl, httpContent);
        var reponse = await httpResponse.Content.ReadAsStringAsync();
        var userDto = JsonSerializer.Deserialize<UserResponse>(reponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDto.Token);

        requestUrl = $"{apiRoute}";
        var memberDto = new MemberResponse
        {
            Introduction = introduction,
            Interests = interests,
            LookingFor = lookingFor,
            City = city,
            Country = country
        };

        memberObjetct = GetMemberObject(memberDto);
        httpContent = GetHttpContent(memberObjetct);

        // Act
        httpResponse = await _client.PutAsync(requestUrl, httpContent);

        // Assert
        Assert.Equal(Enum.Parse<HttpStatusCode>(statusCode, true), httpResponse.StatusCode);
        Assert.Equal(statusCode, httpResponse.StatusCode.ToString());
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

    private static string GetMemberObject(MemberResponse memberDto)
    {
        var entityObject = new JObject()
        {
            { nameof(memberDto.Introduction), memberDto.Introduction },
            { nameof(memberDto.LookingFor), memberDto.LookingFor },
            { nameof(memberDto.Interests), memberDto.Interests },
            { nameof(memberDto.City), memberDto.City },
            { nameof(memberDto.Country), memberDto.Country }
        };

        return entityObject.ToString();
    }

    private static StringContent GetHttpContent(string objectToCode)
    {
        return new StringContent(objectToCode, Encoding.UTF8, "application/json");
    }

    #endregion
}