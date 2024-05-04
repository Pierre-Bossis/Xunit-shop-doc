using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.BLL.Interfaces;
using Shop.DAL.Entities;
using Shop.Models;
using Shop.Models.Article;
using Shop.Models.User;
using Shop.Tools;
using Shop.Tools.Mappers;
using Shop.Tools.Mappers.Article;
using Shop.Tools.Mappers.User;
using System.Security.Claims;

namespace Shop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserBLLRepository _repo;
        private readonly IBasketBLLRepository _basketRepo;
        private readonly JwtGenerator _jwtGenerator;

        public AuthController(IUserBLLRepository repo, IBasketBLLRepository basketRepo, JwtGenerator jwt)
        {
            _repo = repo;
            _basketRepo = basketRepo;
            _jwtGenerator = jwt;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginFormDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            ConnectedUserDTO user = _repo.Login(dto.Email, dto.MotDePasse).ToDto();
            if (user is not null)
            {
                string token = _jwtGenerator.Generate(user);

                return Ok(token);
            }
            return BadRequest(new { success = false, message = "L'email ou le mot de passe ne correspondent pas." });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterFormDTO form)
        {
            if (!ModelState.IsValid) return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            ConnectedUserDTO user = _repo.Register(form.Email, form.Nom, form.Prenom, form.MotDePasse).ToDto();

            if (user is not null)
            {
                string token = _jwtGenerator.Generate(user);

                return Ok(token);
            }

            return StatusCode(500, new { success = false, message = "Erreur lors de la création de l'utilisateur." });


        }
        [Authorize("connectedPolicy")]
        [HttpPost("addtobasket")]
        public IActionResult AddToBasket([FromBody] int reference)
        {
            if (reference == 0) return BadRequest(new { message = "Aucun id ou référence d'article." });

            Guid.TryParse(User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"), out Guid id);



            _basketRepo.AddToBasket(id, reference);
            return Ok();
        }

        [Authorize("connectedPolicy")]
        [HttpGet("getbasket")]
        public IActionResult GetBasket()
        {
            Guid.TryParse(User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"), out Guid id);

            IEnumerable<BasketItemDTO> basket = _basketRepo.GetBasket(id).Select(e => e.ToDto());


            return Ok(basket);
        }
    }
}
