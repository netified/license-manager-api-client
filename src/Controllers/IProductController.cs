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
    /// <summary>
    /// Product controller abstraction
    /// </summary>
    public interface IProductController : IApiController, IApiSearchScoped<ProductDto>
    {
        /// <summary>
        /// List all the products.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="sorts">The sorts.</param>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="headers">The headers.</param>
        [Get("/tenants/{tenantId}/products?filters={filters}&sorts={sorts}&page={page}&pageSize={pageSize}")]
        new Task<PagedResult<ProductDto>> ListAsync(Guid tenantId, string filters = "", string sorts = "", int? page = 1, int? pageSize = 100, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Add a product.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="request">The product request.</param>
        /// <param name="headers">The headers.</param>
        [Post("/tenants/{tenantId}/products")]
        Task<ProductDto> AddAsync(Guid tenantId, ProductRequest request, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Import a product.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="product">The product configuration.</param>
        /// <param name="headers">The headers.</param>
        [Post("/tenants/{tenantId}/products/import")]
        Task<ProductDto> ImportAsync(Guid tenantId, ProductBackupDto product, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// List of product's user permissions.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="headers">The headers.</param>
        [Get("/products/{productId}/permissions")]
        Task<List<PermissionDto>> ListPermissionAsync(Guid productId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Get a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="headers">The headers.</param>
        [Get("/products/{productId}")]
        Task<ProductDto> GetAsync(Guid productId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Export a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="headers">The headers.</param>
        [Post("/products/{productId}/export")]
        Task<ProductBackupDto> ExportAsync(Guid productId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Delete a product.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="headers">The headers.</param>
        [Delete("/products/{productId}")]
        Task DeleteAsync(Guid productId, [HeaderCollection] IDictionary<string, string> headers = default);
    }
}