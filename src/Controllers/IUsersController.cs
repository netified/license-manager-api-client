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

using LicenseManager.Api.Abstractions;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenseManager.Api.Client.Models
{
    public interface IUsersController : IApiController, IApiSearchDefault<UserDto>
    {
        /// <summary>
        /// List all users.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <param name="sorts">The sorts.</param>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="headers">The headers.</param>
        [Get("/users?filters={filters}&sorts={sorts}&page={page}&pageSize={pageSize}")]
        new Task<PagedResult<UserDto>> PaginedListAsync(string filters = "", string sorts = "", int? page = 1, int? pageSize = 100, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Get the current user.
        /// </summary>
        /// <param name="headers">The headers.</param>
        [Get("/users/me")]
        Task<UserDto> GetCurrentAsync([HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Update the default tenant of the current user.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="headers">The headers.</param>
        [Put("/users/me/tenants/{tenantId}")]
        Task<UserDto> SetDefaultTenantAsync(Guid tenantId, [HeaderCollection] IDictionary<string, string> headers = default);
    }
}