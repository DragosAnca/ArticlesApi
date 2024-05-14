using ArticlesApi.Interfaces;
using System.Linq;

namespace ArticlesApi.Models.Validation
{
    public class ArticleValidator
    {
        private readonly IArticlesRepository articlesRepository;
        private List<string> forbiddenWordsExample = new List<string>();

        public ArticleValidator(IArticlesRepository articlesRepository)
        {
            articlesRepository = this.articlesRepository;
            //TODO Could add in this list words from a configurable file in order to keep the code clean
            forbiddenWordsExample.Add("exampleOfForbiddenWord");
        }

        public bool ValidateArticle(Article article)
        {
            return ValidateUniqueness(article) && ValidateForbiddenWords(article);
        }

        /// <summary>
        /// Checks if the given Article is unique by verifying against the database it's id and title.
        /// Both values need to be unique
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if it is unique, false otherwise</returns>
        private bool ValidateUniqueness(Article article)
        {
            return articlesRepository.GetById(article.Id) == null 
                && !articlesRepository.GetArticles(1,int.MaxValue).Any(x => x.Title.Equals(article.Title));
        }

        /// <summary>
        /// Checks the title and contents of the article if it contains any forbidden words.
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if the article is clean, false otherwise</returns>
        private bool ValidateForbiddenWords(Article article)
        {
            bool containsForbiddenWord = forbiddenWordsExample.Any(word => article.Title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                && forbiddenWordsExample.Any(word => article.Content.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);

            return !containsForbiddenWord;
        }

        //TODO Add more validation for Adding/Updating Articles
    }
}
