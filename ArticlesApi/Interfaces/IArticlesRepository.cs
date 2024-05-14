using ArticlesApi.Models;

namespace ArticlesApi.Interfaces
{
    public interface IArticlesRepository
    {
        IEnumerable<Article> GetAll();
        Article GetById(Guid id);
        void Add(Article article);
        void Update(Guid id, Article article);
        void Delete(Guid id);
    }
}
