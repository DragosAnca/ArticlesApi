using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using MongoDB.Driver;

namespace ArticlesApi.DAL
{
    public class ArticlesRepository : IArticlesRepository
    {
        private readonly IMongoCollection<Article> articles;

        public ArticlesRepository(ArticlesContext context)
        {
            articles = context.Articles;
        }

        public IEnumerable<Article> GetAll() => articles.Find(article => true).ToList();

        public Article GetById(Guid id) => articles.Find<Article>(article => article.Id == id).FirstOrDefault();

        public void Add(Article article) => articles.InsertOne(article);

        public void Update(Guid id, Article article) => articles.ReplaceOne(a => a.Id == id, article);

        public void Delete(Guid id) => articles.DeleteOne(article => article.Id == id);
    }
}
