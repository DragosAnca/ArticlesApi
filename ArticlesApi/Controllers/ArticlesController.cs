using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using Asp.Versioning;
using ArticlesApi.Models.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesApi.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{version:apiVersion}/articles")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository articleRepository;
        private readonly ArticleValidator articleValidator;

        public ArticlesController(IArticlesRepository repository)
        {
            this.articleRepository = repository;
            articleValidator = new ArticleValidator(repository);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Article>> GetArticles(int pageNumber = 1, int pageSize = 10)
        {

            var articles = articleRepository.GetArticles(pageNumber, pageSize);
            var totalArticles = articleRepository.GetTotalArticlesCount();

            var paginationMetadata = new
            {
                TotalCount = totalArticles,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalArticles / (double)pageSize)
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(articles);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var article = articleRepository.GetById(id);
            if (article == null) return NotFound(new { Message = "Article not found" });
            return Ok(article);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Article article)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            article.Id = Guid.NewGuid(); // Ensure Id is generated here
            if (!articleValidator.ValidateArticle(article))
            {
                return BadRequest(new { Message = "Validation failed" });
            }
            articleRepository.Add(article);
            return CreatedAtAction(nameof(GetById), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] Article article)
        {
            if (id != article.Id) return BadRequest(new { Message = "ID mismatch" });
            if (!articleValidator.ValidateArticle(article))
            {
                return BadRequest(new { Message = "Validation failed" });
            }
            articleRepository.Update(id, article);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            articleRepository.Delete(id);
            return NoContent();
        }
    }
}
