// Copyright (c) 2022 Netified <contact@netified.io>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.

using LicenseManager.Api.Client.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace LicenseManager.Api.Client.Extensions
{
    public static class ApiClientExtensions
    {
        /// <summary>
        /// Adds the license manager client.
        /// </summary>
        /// <typeparam name="ApiClientHandler">The type of the pi handler.</typeparam>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The product configuration.</param>
        /// <param name="configSection">The configuration section.</param>
        /// <param name="refitSettings">The refit settings.</param>
        public static void AddLicenseManagerClient<ApiClientHandler>(
            this IServiceCollection services,
            IConfiguration configuration,
            string configSection = nameof(ApiClientConfiguration),
            RefitSettings refitSettings = null)
            where ApiClientHandler : DelegatingHandler
        {
            // Load client configuration
            var clientConfiguration = new ApiClientConfiguration();
            configuration.GetSection(configSection).Bind(clientConfiguration);

            // Register all controllers.
            foreach (var controller in ListApiController())
            {
                services.AddRefitClient(controller, refitSettings)
                    .ConfigureHttpClient(c => c.BaseAddress = clientConfiguration.BaseAddress)
                    .AddPolicyHandler(GetRetryPolicy(clientConfiguration.RetryConfiguration))
                    .AddPolicyHandler(GetTimeoutPolicy(clientConfiguration.RetryConfiguration))
                    .AddHttpMessageHandler<ApiClientHandler>();
            }
        }

        /// <summary>
        /// Adds the license manager client.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">The product configuration.</param>
        /// <param name="configSection">The configuration section.</param>
        /// <param name="refitSettings">The refit settings.</param>
        public static void AddLicenseManagerClient(
            this IServiceCollection services,
            IConfiguration configuration,
            string configSection = nameof(ApiClientConfiguration),
            RefitSettings refitSettings = null)
        {
            // Load client configuration
            var clientConfiguration = new ApiClientConfiguration();
            configuration.GetSection(configSection).Bind(clientConfiguration);

            // Register all controllers.
            foreach (var controller in ListApiController())
            {
                services.AddRefitClient(controller, refitSettings)
                    .ConfigureHttpClient(c => c.BaseAddress = clientConfiguration.BaseAddress)
                    .AddPolicyHandler(GetRetryPolicy(clientConfiguration.RetryConfiguration))
                    .AddPolicyHandler(GetTimeoutPolicy(clientConfiguration.RetryConfiguration));
            }
        }

        /// <summary>
        /// Lists the API controllers.
        /// </summary>
        /// <returns></returns>
        private static List<Type> ListApiController()
        {
            var interfaceType = typeof(IApiController);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(interfaceType.IsAssignableFrom).ToList();
        }

        /// <summary>
        /// Gets the http retry policy.
        /// </summary>
        /// <param name="pollyConfiguration">The polly configuration.</param>
        /// <returns></returns>
        private static Polly.Retry.AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy(ApiClientRetryConfiguration pollyConfiguration)
            => HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>()
            .WaitAndRetryAsync(pollyConfiguration.RetryCount, _ => TimeSpan.FromMilliseconds(pollyConfiguration.SleepDuration));

        /// <summary>
        /// Gets the http timeout policy.
        /// </summary>
        /// <param name="pollyConfiguration">The polly configuration.</param>
        /// <returns></returns>
        private static AsyncTimeoutPolicy<HttpResponseMessage> GetTimeoutPolicy(ApiClientRetryConfiguration pollyConfiguration)
            => Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMilliseconds(pollyConfiguration.Timeout));
    }
}