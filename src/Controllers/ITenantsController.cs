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
    public interface ITenantsController : IApiController, IApiSearchDefault<LicenseDto>
    {
        /// <summary>
        /// List all tenants.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <param name="sorts">The sorts.</param>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="headers">The headers.</param>
        [Get("/tenants?filters={filters}&sorts={sorts}&page={page}&pageSize={pageSize}")]
        new Task<PagedResult<TenantDto>> ListAsync(string filters = "", string sorts = "", int? page = 1, int? pageSize = 100, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Create a tenant.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="headers">The headers.</param>
        [Post("/tenants")]
        Task<TenantDto> AddAsync(TenantRequest request, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Getting a tenant.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="headers">The headers.</param>
        [Get("/tenants/{tenantId}")]
        Task<TenantDto> GetAsync(Guid tenantId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Delete the tenant.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="headers">The headers.</param>
        [Delete("/tenants/{tenantId}")]
        Task DeleteAsync(Guid tenantId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// List of tenant's user permissions.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="headers">The headers.</param>
        [Get("/tenants/{tenantId}/permissions")]
        Task<List<PermissionDto>> ListPermissionAsync(Guid tenantId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Add a user permission to the tenant.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="request">The permission request.</param>
        /// <param name="headers">The headers.</param>
        [Post("/tenants/{tenantId}/permissions")]
        Task AddPermissionAsync(Guid tenantId, TenantMemberRequest request, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Remove the user's permission from the tenant.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="permissionId">The permission identifier.</param>
        /// <param name = "headers" > The headers.</param>
        [Delete("/tenants/{tenantId}/permissions/{permissionId}")]
        Task RemovePermissionAsync(Guid tenantId, Guid permissionId, [HeaderCollection] IDictionary<string, string> headers = default);
    }
}