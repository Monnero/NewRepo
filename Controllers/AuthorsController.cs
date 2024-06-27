using HotelWebApi.Data;
using HotelWebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return Ok(await _authorRepository.GetAuthorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAuthorAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            if (author.IsValidate())
            {
                await _authorRepository.InsertAuthorAsync(author);
                await _authorRepository.SaveAsync();
                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            else
            {
                return BadRequest("Author is not valid.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            if (author.IsValidate())
            {
                await _authorRepository.UpdateAuthorAsync(author);
                await _authorRepository.SaveAsync();
                return NoContent();
            }
            else
            {
                return BadRequest("Author is not valid.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorRepository.DeleteAuthorAsync(id);
            await _authorRepository.SaveAsync();
            return NoContent();
        }
    }
}