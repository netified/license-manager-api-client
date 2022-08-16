# Netified.LicenseManager.Api.Client

[![GitHub](https://img.shields.io/github/license/netified/license-manager-api-client?style=for-the-badge)](https://github.com/netified/license-manager-api-client/blob/main/LICENSE)
[![GitHub Sponsors](https://img.shields.io/github/sponsors/netified?style=for-the-badge)](https://github.com/sponsors/thomas-illiet/)
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/netified/license-manager-api-client/Continuous%20integration?style=for-the-badge)](https://github.com/netified/license-manager-api-client/actions/workflows/ci.yml)
[![Nuget](https://img.shields.io/nuget/dt/Netified.LicenseManager.Api.Client?style=for-the-badge)](https://www.nuget.org/packages/Netified.LicenseManager.Api.Client/)
[![Nuget](https://img.shields.io/nuget/v/Netified.LicenseManager.Api.Client?style=for-the-badge)](https://www.nuget.org/packages/Netified.LicenseManager.Api.Client/)

A .NET Standard and .NET Core License Manager REST API toolkit and API wrapper.

## Installation

The library is available as a nuget package. You can install it as any other nuget package from your IDE, try to search by `Netified.LicenseManager.Api.Client`. You can find package details [on this web page](https://www.nuget.org/packages/Netified.LicenseManager.Api.Client).

```xml
// Package Manager
Install-Package Netified.LicenseManager.Api.Client

// .NET CLI
dotnet add package Netified.LicenseManager.Api.Client

// Package reference in .csproj file
<PackageReference Include="Netified.LicenseManager.Api.Client" Version="1.0.0" />
```

Then add the following to your ConfigureServices method.

```csharp
services.AddLicenseManagerClient<MyAPIHandler>(Configuration)
```

The following are required in app settings if you are not using an official installation:

```json
"ApiClientConfiguration":{
    "BaseAddress": "https://api.netified.io/license-manager/v1/"
}
```

## Configuration

By default API requests to License Manager must be authenticated, you can find below an handler to add authentication on your API requests.

```csharp
public class MyAPIHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<MyAPIHandler> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MyAPIHandler"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    /// <param name="logger">The logger.</param>
    public MyAPIHandler(IHttpContextAccessor httpContextAccessor, ILogger<MyAPIHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    /// <summary>
    /// Sends an HTTP request to the inner handler to send to the server as an asynchronous operation.
    /// </summary>
    /// <param name="request">The HTTP request message to send to the server.</param>
    /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
    /// <returns>
    /// The task object representing the asynchronous operation.
    /// </returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpResponseMessage responseMessage;
        try
        {
            var accessToken = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
            if (string.IsNullOrEmpty(accessToken))
                throw new Exception($"Access token is missing for the request {request.RequestUri}");

            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            responseMessage = await base.SendAsync(request, cancellationToken);
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to run http query {RequestUri}", request.RequestUri);
            throw;
        }
        return responseMessage;
    }
}
```

## Please show the value

Choosing a project dependency could be difficult. We need to ensure stability and maintainability of our projects.
Surveys show that GitHub stars count play an important factor when assessing library quality.

⭐ Please give this repository a star. It takes seconds and help thousands of developers! ⭐

## Support development

It doesn't matter if you are a professional developer, creating a startup or work for an established company.
All of us care about our tools and dependencies, about stability and security, about time and money we can safe, about quality we can offer.
Please consider sponsoring to give me an extra motivational push to develop the next great feature.

> If you represent a company, want to help the entire community and show that you care, please consider sponsoring using one of the higher tiers.
Your company logo will be shown here for all developers, building a strong positive relation.

## How to Contribute

Everyone is welcome to contribute to this project! Feel free to contribute with pull requests, bug reports or enhancement suggestions.

## Bugs and Feedback

For bugs, questions and discussions please use the [GitHub Issues](https://github.com/netified/license-manager-api-client/issues).

## License

This project is licensed under [MIT License](https://github.com/netified/license-manager-api-client/blob/main/LICENSE).
