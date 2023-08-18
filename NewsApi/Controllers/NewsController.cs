using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Core.Entity;
using News.Core.Repos;
using News.Core.Specifications;
using NewsApi.Dtos;

namespace NewsApi.Controllers
{

	public class NewsController : BaseApiController
	{
		private readonly IGenericRepository<News.Core.Entity.News> _newsRepository;
		private readonly IMapper _mapper;

		public NewsController(IGenericRepository<News.Core.Entity.News> newsRepository,
			 IMapper mapper)
		{
			_newsRepository = newsRepository;
			_mapper = mapper;
		}

		[HttpPost("Create")]
		public async Task<ActionResult<News.Core.Entity.News>> CreateNewsAsync(NewsDtoV2 news)
		{
			News.Core.Entity.News news1 = new News.Core.Entity.News()
			{
				Title = news.Title,
				Image = news.Image,
				AuthorId = news.AuthorId,
				publicationDate = news.PublicationDate
			};

			await _newsRepository.Add(news1);

			await _newsRepository.UpdateDB();
			return Ok(news1);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteNewsAsync(int id)
		{
			var data = await _newsRepository.FindEntityById(id);
			_newsRepository.Delete(data);
			await _newsRepository.UpdateDB();

			return Ok();
		}

		[HttpPost("Update")] // ..api/Author/Update
		public async Task<ActionResult<NewsDtoV3>> UpdateNewsAsync(NewsDtoV4 news)
		{
			//var data = _mapper.Map< NewsDto, News.Core.Entity.News>(news);
			var data = new News.Core.Entity.News()
			{
				Title = news.Title,
				Image = news.Image,
				AuthorId = news.AuthorId,
				publicationDate = news.PublicationDate,
				CreationDate = news.CreationDate,
				Id = news.Id
			};

			_newsRepository.Update(data);
			await _newsRepository.UpdateDB();
			return Ok(news);
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<NewsDto>>> GetAllNews(string? Order, string? Author, string? title)
		{
			NewsWithSpecifications spec = new NewsWithSpecifications(Order, Author, title);

			var result = await _newsRepository.GetAllWithSpecAsync(spec);
			var data = _mapper.Map<IReadOnlyList<News.Core.Entity.News>, IReadOnlyList<NewsDto>>(result);

			return Ok(data);
		}

		[HttpGet("{Id}")]
		public async Task<ActionResult<NewsDtoV3>> GetById(int id)
		{
			var spec = new NewsWithSpecifications(id);
			var data = await _newsRepository.GetAllWithSpecAsync(spec);
			var Fdata = data.FirstOrDefault();
			if (Fdata is null) return BadRequest();
			var result = _mapper.Map<News.Core.Entity.News, NewsDtoV3>(Fdata);
			return Ok(result);
		}
	}
}
