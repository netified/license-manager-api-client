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
    public interface ITenantsController : IApiController
    {
        /// <summary>
        /// Lists the tenants asynchronous.
        /// </summary>
        /// <returns></returns>
        [Get("/tenants")]
        Task<PagedResult<TenantDto>> ListAsync();

        /// <summary>
        /// Adds the tenant asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Post("/tenants")]
        Task<TenantDto> AddAsync(TenantRequest request);

        /// <summary>
        /// Gets the tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        [Get("/tenants/{tenantId}")]
        Task<TenantDto> GetAsync(Guid tenantId);

        /// <summary>
        /// Deletes the tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        [Delete("/tenants/{tenantId}")]
        Task DeleteAsync(Guid tenantId);

        /// <summary>
        /// Lists the tenant members asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <returns></returns>
        [Get("/tenants/{tenantId}/members")]
        Task<List<UserTenantDto>> ListMemberAsync(Guid tenantId);

        /// <summary>
        /// Adds the member in the tenant asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [Get("/tenants/{tenantId}/members")]
        Task<UserTenantDto> AddMemberAsync(Guid tenantId, TenantMemberRequest request);

        /// <summary>
        /// Deletes the tenant member asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [Get("/tenants/{tenantId}/members/{userId}")]
        Task DeleteMemberAsync(Guid tenantId, Guid userId);

        /// <summary>
        /// Updates the role of a tenant member asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        [Put("/tenants/{tenantId}/members/{userId}/role")]
        Task UpdateRoleMemberAsync(Guid tenantId, Guid userId, TenantRoleType role);
    }
}