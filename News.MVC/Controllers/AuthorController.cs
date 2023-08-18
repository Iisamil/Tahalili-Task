using Microsoft.AspNetCore.Mvc;
using News.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Text;

namespace News.MVC.Controllers
{
	public class AuthorController : Controller
	{
		Uri baseAdress = new Uri("https://localhost:7200/api");
		private readonly HttpClient _Client;
        public AuthorController()
        {
            _Client = new HttpClient();
			_Client.BaseAddress = baseAdress;
        }

		[HttpGet]
		public IActionResult GetAuthors()
		{
			List<AuthorViewModel> Authorlist = new List<AuthorViewModel>();
			HttpResponseMessage response = _Client.GetAsync(baseAdress +
				"/Author/GetAllAuthors").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				Authorlist = JsonConvert.DeserializeObject<List<AuthorViewModel>>(data);
			}
			return View(Authorlist);
		}

        [HttpPost]
        public IActionResult GetAuthors(SpecParam specParam)
        {
            List<AuthorViewModel> Authorlist = new List<AuthorViewModel>();
			HttpResponseMessage response;

            if (specParam.filterCharacter == null) {
                 response = _Client.GetAsync(baseAdress +
               $"/Author/GetAllAuthors?Order={specParam.Order}&filterCharacter=").Result;
            } 
			else 
			{
                response = _Client.GetAsync(baseAdress +
                $"/Author/GetAllAuthors?Order={specParam.Order}&filterCharacter={specParam.filterCharacter.FirstOrDefault()}").Result;
            }
             
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Authorlist = JsonConvert.DeserializeObject<List<AuthorViewModel>>(data);
            }
            return View(Authorlist);
        }

        [HttpGet] // Default
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(AuthorViewModel authorViewModel)
		{

			string data = JsonConvert.SerializeObject(authorViewModel.Name);
			StringContent content = new StringContent(data, Encoding.UTF8,
				"application/json");
			string data1 = authorViewModel.Name;
			HttpResponseMessage response = _Client.PostAsync(baseAdress +
				$"/Author/CreateAuthor/Create?Name={data1}",content).Result;

			if (response.IsSuccessStatusCode)
			{
				//var resut = response.RequestMessage.Properties;
				return RedirectToAction(nameof(GetAuthors));
				
			}
			
			return View(authorViewModel);
		}

		[HttpGet] // Default
		public IActionResult Update(int? Id)
		{
			if (Id == null) return BadRequest();
			AuthorViewModel Authorlist = new AuthorViewModel();
			HttpResponseMessage response = _Client.GetAsync(baseAdress +
				$"/Author/GetById?Id={Id}").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				Authorlist = JsonConvert.DeserializeObject<AuthorViewModel>(data);
				return View(Authorlist);
			}
			return View( );
			
		}

		[HttpPost]
		public IActionResult Update(AuthorViewModel authorViewModel)
		{

			string data = JsonConvert.SerializeObject(authorViewModel);
			StringContent content = new StringContent(data, Encoding.UTF8,
				"application/json");
			//string data1 = authorViewModel.Name;
			HttpResponseMessage response = _Client.PostAsync(baseAdress +
				"/Author/UpdateAuthor/Update",content).Result;

			if (response.IsSuccessStatusCode)
			{
				//var resut = response.RequestMessage.Properties;
				return RedirectToAction(nameof(GetAuthors));
				
			}
			
			return View(authorViewModel);
		}

		[HttpGet] // Default
		public IActionResult Delete(int? Id)
		{
			if (Id == null) return BadRequest();
			AuthorViewModel Authorlist = new AuthorViewModel();
			HttpResponseMessage response = _Client.GetAsync(baseAdress +
				$"/Author/GetById/{Id}").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				Authorlist = JsonConvert.DeserializeObject<AuthorViewModel>(data);
				return View(Authorlist);
			}
			return View();

		}

		[HttpGet] // Default
		public IActionResult Details(int? Id)
		{
			if (Id == null) return BadRequest();
			AuthorViewModel Authorlist = new AuthorViewModel();
			HttpResponseMessage response = _Client.GetAsync(baseAdress +
				$"/Author/GetById/{Id}").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				Authorlist = JsonConvert.DeserializeObject<AuthorViewModel>(data);
				return View(Authorlist);
			}
			return View();

		}

		[HttpPost]
		public IActionResult Delete(AuthorViewModel authorViewModel)
		{
			
			HttpResponseMessage response = _Client.DeleteAsync(baseAdress +
				$"/Author/DeleteAuthor/{authorViewModel.Id}" ).Result;

			if (response.IsSuccessStatusCode)
			{
				//var resut = response.RequestMessage.Properties;
				return RedirectToAction(nameof(GetAuthors));

			}

			return View(authorViewModel);
		}


	}
}
