using ArticlesApi.DAL;
using ArticlesApi.Interfaces;
using ArticlesApi.Models;
using ArticlesApi.Models.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticlesApi.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticlesController : Controller
    {
        private readonly IArticlesRepository repository;
        private readonly ArticleValidator articleValidator;

        public ArticlesController(IArticlesRepository repository)
        {
            this.repository = repository;
            articleValidator = new ArticleValidator(repository);
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(repository.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var article = repository.GetById(id);
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
            repository.Add(article);
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
            repository.Update(id, article);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            repository.Delete(id);
            return NoContent();
        }
    }
}
