using ArticlesApi.Interfaces;
using System.Linq;
using System.Net;

namespace ArticlesApi.Models.Validation
{
    /// <summary>
    /// Validator class to be used when adding or modyfing Articles
    /// </summary>
    public class ArticleValidator
    {
        private readonly IArticlesRepository articlesRepository;
        private List<string> forbiddenWordsExample = new List<string>();

        public ArticleValidator(IArticlesRepository articlesRepository)
        {
            this.articlesRepository = articlesRepository;
            //TODO Could add in this list words from a configurable file in order to keep the code clean
            forbiddenWordsExample.Add("exampleOfForbiddenWord");
        }

        public string ValidateArticle(Article article)
        {
            if (!ValidateUniqueness(article))
            {
                return "Id or title already exists";

            }
            if(!ValidateForbiddenWords(article))
            {
                return "Title or content contains forbidden words";

            }
            return null;
        }

        /// <summary>
        /// Checks if the given Article is unique by verifying against the database it's id and title.
        /// Both values need to be unique
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if it is unique, false otherwise</returns>
        private bool ValidateUniqueness(Article article)
        {
            bool isIdUnique = articlesRepository.GetById(article.Id) == null;
            bool isTitleUnique = !articlesRepository.GetArticles(0, 0).Any(x => x.Title.Equals(article.Title));
            return isIdUnique && isTitleUnique;
        }

        /// <summary>
        /// Checks the title and contents of the article if it contains any forbidden words.
        /// </summary>
        /// <param name="article"></param>
        /// <returns>true if the article is clean, false otherwise</returns>
        private bool ValidateForbiddenWords(Article article)
        {
            bool cleanTitle = !forbiddenWordsExample.Any(word => article.Title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);
            bool cleanContent = !forbiddenWordsExample.Any(word => article.Content.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0);

            return cleanTitle && cleanContent;
        }

        //TODO Add more validation for Adding/Updating Articles
    }
}
