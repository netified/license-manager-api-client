﻿// Copyright (c) 2022 Netified <contact@netified.io>
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
using System.Threading.Tasks;

namespace LicenseManager.Api.Client.Models
{
    /// <summary>
    /// Product controller abstraction
    /// </summary>
    public interface IProductController : IApiController
    {
        /// <summary>
        /// Lists the products asynchronous.
        /// </summary>
        /// <returns></returns>
        [Get("/tenants/{tenantId}/products")]
        Task<PagedResult<ProductDto>> ListAsync();

        /// <summary>
        /// Adds the product asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="request">The product request.</param>
        /// <returns></returns>
        [Post("/tenants/{tenantId}/products")]
        Task<ProductDto> AddAsync(Guid tenantId, ProductRequest request);

        /// <summary>
        /// Imports the product asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="request">The product request.</param>
        /// <returns></returns>
        [Post("/tenants/{tenantId}/products/import")]
        Task<ProductDto> ImportAsync(Guid tenantId, ProductBackupDto request);

        /// <summary>
        /// Exports the product asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [Post("/tenants/{tenantId}/products/{productIdm" +
            "}/export")]
        Task<ProductBackupDto> ExportAsync(Guid tenantId, Guid productId);

        /// <summary>
        /// Gets the product asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [Get("/tenants/{tenantId}/products/{productId}")]
        Task<ProductDto> GetAsync(Guid tenantId, Guid productId);

        /// <summary>
        /// Deletes the product asynchronous.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        [Delete("/tenants/{tenantId}/products/{productId}")]
        Task DeleteAsync(Guid tenantId, Guid productId);
    }
}