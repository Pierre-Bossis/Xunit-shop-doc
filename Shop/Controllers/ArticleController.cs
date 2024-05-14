using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using Shop.Models.Article;
using Shop.Tools;
using Shop.Tools.Mappers.Article;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleBLLRepository _repo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticleController(IArticleBLLRepository repo, IWebHostEnvironment hostingEnvironment)
        {
            _repo = repo;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Create([FromForm] ArticleCreateDTO article)
        {
            if(!ModelState.IsValid) return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            string relativePath = await ImageConverter.SaveIcone(article.Image, "Articles", _hostingEnvironment);

            bool success = _repo.Create(article.ToEntity(relativePath));
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
           string ImageToDelete = _repo.DeleteByReference(reference);
           string repertoireTravail = Directory.GetCurrentDirectory();
           string cheminAbsolu = Path.Combine(repertoireTravail, ImageToDelete);

            if (System.IO.File.Exists(cheminAbsolu))
                System.IO.File.Delete(cheminAbsolu);

            if (ImageToDelete is not null)
                return Ok("Article supprimé avec succès");
            return NotFound("Article non trouvé.");
        }
    }
}
