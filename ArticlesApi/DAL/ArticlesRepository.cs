using Amazon.Runtime.Internal.Util;
using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;

namespace ArticlesApi.DAL
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly IMongoCollection<Article> articles;
        private readonly IMemoryCache cache;
        private readonly TimeSpan cacheExpiration = TimeSpan.FromMinutes(5);
        private readonly List<string> cacheKeys = new List<string>();

        public ArticlesRepository(ArticlesContext context, IMemoryCache cache)
        {
            articles = context.Articles;
            this.cache = cache;
        }

        public IEnumerable<Article> GetArticles(int pageNumber, int pageSize)
        {
            var cacheKey = $"articles_{pageNumber}_{pageSize}";
            if (!cacheKeys.Contains(cacheKey)) cacheKeys.Add(cacheKey);

            if (!cache.TryGetValue(cacheKey, out List<Article> cachedArticles))
            {
                cachedArticles = articles.Find(article => true)
                                          .Skip((pageNumber - 1) * pageSize)
                                          .Limit(pageSize)
                .ToList();

                cache.Set(cacheKey, cachedArticles, cacheExpiration);
            }

            return cachedArticles;
        }
        public Article GetById(Guid id)
        {
            var cacheKey = $"article_{id}";
            if (!cacheKeys.Contains(cacheKey)) cacheKeys.Add(cacheKey);

            if (!cache.TryGetValue(cacheKey, out Article cachedArticle))
            {
                cachedArticle = articles.Find(article => article.Id == id).FirstOrDefault();

                if (cachedArticle != null)
                {
                    cache.Set(cacheKey, cachedArticle, cacheExpiration);
                }
            }

            return cachedArticle;
        }
        public void Add(Article article)
        {
            articles.InsertOne(article);
            InvalidateCache();
        }

        public void Update(Guid id, Article article)
        {
            articles.ReplaceOne(existingArticle => existingArticle.Id == article.Id, article);
            InvalidateCache();
        }

        public void Delete(Guid id)
        {
            articles.DeleteOne(article => article.Id == id);
            InvalidateCache();
        }

        public int GetTotalArticlesCount()
        {
            var cacheKey = "totalArticlesCount";
            if (!cacheKeys.Contains(cacheKey)) cacheKeys.Add(cacheKey);

            if (!cache.TryGetValue(cacheKey, out int totalArticlesCount))
            {
                totalArticlesCount = (int)articles.CountDocuments(article => true);
                cache.Set(cacheKey, totalArticlesCount, cacheExpiration);
            }

            return totalArticlesCount;
        }

        private void InvalidateCache()
        {
            foreach (var cacheKey in cacheKeys)
            {
                cache.Remove(cacheKey);
            }
            cacheKeys.Clear();
        }
    }
}
