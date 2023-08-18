using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Core.Entity;
using News.Core.Repos;
using News.Core.Specifications;

namespace NewsApi.Controllers
{
	public class AuthorController : BaseApiController
	{
		private readonly IGenericRepository<Author> _authorrepository;

		public AuthorController(IGenericRepository<Author> authorrepository) 
		{
			_authorrepository = authorrepository;
		}

		[HttpPost("Create")] // ..api/Author/create
		public async Task<ActionResult<Author>> CreateAuthor(string name)
		{
			Author author = new Author()
			{
				Name = name
			};

			await _authorrepository.Add(author);

			await _authorrepository.UpdateDB();

			return Ok(author);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteAuthorAsync(int id)
		{
			var author = await _authorrepository.FindEntityById(id);
			_authorrepository.Delete(author);
			await _authorrepository.UpdateDB();

			return Ok();
		}

		[HttpPost("Update")] // ..api/Author/Update
		public async Task<ActionResult<Author>> UpdateAuthorAsync(Author author)
		{
		    _authorrepository.Update(author);
			await _authorrepository.UpdateDB();
			return Ok(author);
		}

		[HttpGet]

		public async Task<ActionResult<IReadOnlyList<Author>>> GetAllAuthors([FromQuery]string? Order, Char? filterCharacter)
		{
			AuthorWithSpecifications spec;
			if (filterCharacter == null)
				spec = new AuthorWithSpecifications(Order);
			else 
				spec = new AuthorWithSpecifications(Order, filterCharacter);

			var result = await _authorrepository.GetAllWithSpecAsync(spec);

			return Ok(result);
		}

		[HttpGet("{Id}")]
		public async Task<ActionResult<Author>> GetById (int Id)
		{
			var result = await _authorrepository.FindEntityById(Id);
			return Ok(result);
		}


	}
}
