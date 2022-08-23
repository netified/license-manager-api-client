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
using LicenseManager.Api.Client.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LicenseManager.Api.Client.Extensions
{
    public static class ApiControllerExtensions
    {
        /// <summary>
        /// Get all elements.
        /// </summary>
        /// <typeparam name="ApiController">The type of the api controller.</typeparam>
        /// <typeparam name="ApiEntity">The type of the api entity.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="sorts">The sorts.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="headers">The headers.</param>
        public static async Task<List<ApiEntity>> ListAsync<ApiController, ApiEntity>(this ApiController controller, string filters = "", string sorts = "", int? pageSize = 100, [HeaderCollection] IDictionary<string, string> headers = default)
            where ApiController : IApiController, IApiSearchDefault<ApiEntity>
            where ApiEntity : class
        {
            var entities = new List<ApiEntity>();
            var page = 1;

            PagedResult<ApiEntity> result;
            do
            {
                result = await controller.PaginedListAsync(filters, sorts, page++, pageSize, headers);
                entities.AddRange(result.Results);
            } while (result.Results.Count != 0);

            return entities;
        }

        /// <summary>
        ///  Get all elements.
        /// </summary>
        /// <typeparam name="ApiController">The type of the api controller.</typeparam>
        /// <typeparam name="ApiEntity">The type of the api entity.</typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="scopeId">The scope identifier.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="sorts">The sorts.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="headers">The headers.</param>
        /// <returns></returns>
        public static async Task<List<ApiEntity>> ListAsync<ApiController, ApiEntity>(this ApiController controller, Guid scopeId, string filters = "", string sorts = "", int? pageSize = 100, [HeaderCollection] IDictionary<string, string> headers = default)
            where ApiController : IApiController, IApiSearchScoped<ApiEntity>
            where ApiEntity : class
        {
            var entities = new List<ApiEntity>();
            var page = 1;

            PagedResult<ApiEntity> result;
            do
            {
                result = await controller.PaginedListAsync(scopeId, filters, sorts, page++, pageSize, headers);
                entities.AddRange(result.Results);
            } while (result.Results.Count != 0);

            return entities;
        }
    }
}