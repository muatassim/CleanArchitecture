using CleanArchitecture.Application.Models;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Interfaces.Messaging;
using CleanArchitecture.Core.Shared;
using System;
namespace CleanArchitecture.Application.Features.Queries.Categories.Get
{
    public class GetCategoriesSearchRequest(SearchRequest searchParameters) : IQuery<Page<CategoriesModel>>, ICacheableRequest
    {
        public SearchRequest SearchParameters { get; set; } = searchParameters;
        public string CacheKey
        {
            get
            {
                return $"SearchModel";
            }
        }
        public TimeSpan Expiration => TimeSpan.FromHours(1);
    }
}
