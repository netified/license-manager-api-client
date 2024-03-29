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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenseManager.Api.Client.Models
{
    public interface ILicensesController : IApiController, IApiSearchScoped<LicenseDto>
    {
        /// <summary>
        /// Get all licenses.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="sorts">The sorts.</param>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="headers">The headers.</param>
        [Get("/products/{productId}/licenses?filters={filters}&sorts={sorts}&page={page}&pageSize={pageSize}")]
        new Task<PagedResult<LicenseDto>> PaginedListAsync(Guid productId, string filters = "", string sorts = "", int? page = 1, int? pageSize = 100, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Get a license.
        /// </summary>
        /// <param name="licenseId">The license identifier.</param>
        /// <param name="headers">The headers.</param>
        [Get("/licenses/{licenseId}")]
        Task<LicenseDto> GetAsync(Guid licenseId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Add a license.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="request">The license request.</param>
        /// <param name="headers">The headers.</param>
        [Post("/products/{productId}/licenses")]
        Task<LicenseDto> AddAsync(Guid productId, LicenseRequest request, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Import a license.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="license">The saved license.</param>
        /// <param name="headers">The headers.</param>
        [Post("/products/{productId}/licenses/import")]
        Task<LicenseDto> ImportAsync(Guid productId, LicenseBackupDto license, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Export a license.
        /// </summary>
        /// <param name="licenseId">The license identifier.</param>
        /// <param name="headers">The headers.</param>
        [Post("/licenses/{licenseId}/export")]
        Task<LicenseBackupDto> ExportAsync(Guid licenseId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Download the license.
        /// </summary>
        /// <param name="licenseId">The license identifier.</param>
        /// <param name="headers">The headers.</param>
        [Get("/licenses/{licenseId}/download")]
        Task<string> DownloadAsync(Guid licenseId, [HeaderCollection] IDictionary<string, string> headers = default);

        /// <summary>
        /// Delete a license.
        /// </summary>
        /// <param name="licenseId">The license identifier.</param>
        /// <param name="headers">The headers.</param>
        [Delete("/licenses/{licenseId}")]
        Task DeleteAsync(Guid licenseId, [HeaderCollection] IDictionary<string, string> headers = default);
    }
}