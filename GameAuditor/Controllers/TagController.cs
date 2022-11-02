using GameAuditor.Models.ViewModels;
using GameAuditor.Models;
using GameAuditor.Repositories.Implimentations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using GameAuditor.Repositories.Interfaces;
using GameAuditor.Services.UserService;
using Microsoft.AspNetCore.Identity;

namespace GameAuditor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : Controller
    {
        public IEntityRepository<PostTag> _entityRepository;

        public TagController(IEntityRepository<PostTag> repository)
        {
            _entityRepository = repository;
        }

        [HttpPost]
        public IActionResult Create(PostTag entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _entityRepository.Create(new PostTag() {Tag = entity.Tag});
                _entityRepository.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{getalltags}")]
        public Post GetAllTags()
        {
            return _entityRepository.GetAllTags();
        }
    }
}
