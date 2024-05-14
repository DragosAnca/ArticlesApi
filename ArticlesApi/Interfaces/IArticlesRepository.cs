using ArticlesApi.Models;

namespace ArticlesApi.Interfaces
{
    public interface IArticlesRepository
    {
        IEnumerable<Article> GetArticles(int pageNumber, int pageSize);
        Article GetById(Guid id);
        void Add(Article article);
        void Update(Guid id, Article article);
        void Delete(Guid id);
        int GetTotalArticlesCount();

    }
}
