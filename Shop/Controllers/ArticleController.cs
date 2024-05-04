using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using Shop.Models.Article;
using Shop.Tools.Mappers.Article;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleBLLRepository _repo;

        public ArticleController(IArticleBLLRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<ArticleDTO> articles =  _repo.GetAll().Select(e => e.ToDtoGET());
            return Ok(articles);
        }

        [HttpGet("search/{nom}")]
        public IActionResult Search([FromRoute] string nom)
        {
            IEnumerable<ArticleDTO> searchResult = _repo.Search(nom).Select(e => e.ToDtoGET());

            return Ok(searchResult);
        }

        [HttpGet("{reference:int}")]
        public IActionResult GetByReference(int reference)
        {
            ArticleDTO article = _repo.GetByReference(reference).ToDtoGET();
            if(article is not null)
                return Ok(article);
            return NotFound("Aucun article trouvé.");
        }

        [Authorize("AdminPolicy")]
        [HttpPost("create")]
        public IActionResult Create([FromBody] ArticleCreateDTO article)
        {
            if(!ModelState.IsValid) return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            bool success = _repo.Create(article.ToEntity());
            if(success)
                return Created("", new { success = true, message = "Article créé avec succès." });
            return StatusCode(500, new { success = false, message = "Erreur lors de la création de l'article." });
        }

        [Authorize("AdminPolicy")]
        [HttpPut("update/{reference:int}")]
        public IActionResult Update([FromBody] ArticleUpdateDTO article)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            bool success = _repo.Update(article.ToEntity());
            if (success)
                return Ok("Article modifié avec succès.");
            return BadRequest("Erreur lors de la mise à jour de l'article.");
        }

        [Authorize("AdminPolicy")]
        [HttpDelete("delete/{reference:int}")]
        public IActionResult DeleteByReference(int reference)
        {
           bool success = _repo.DeleteByReference(reference);
            if (success)
                return Ok("Article supprimé avec succès");
            return NotFound("Article non trouvé.");
        }
    }
}
