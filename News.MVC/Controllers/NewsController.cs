using Microsoft.AspNetCore.Mvc;
using News.MVC.Helper;
using News.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace News.MVC.Controllers
{
	public class NewsController : Controller
	{
		Uri baseAdress = new Uri("https://localhost:7200/api");
		private readonly HttpClient _Client;
		public NewsController()
		{
			_Client = new HttpClient();
			_Client.BaseAddress = baseAdress;
		}

		[HttpGet]
		public IActionResult Index()
		{
			List<NewsViewModelV2> Newslist = new List<NewsViewModelV2>();
			HttpResponseMessage response = _Client.GetAsync(baseAdress +
				"/News/GetAllNews").Result;
			if (response.IsSuccessStatusCode)
			{
				string data = response.Content.ReadAsStringAsync().Result;
				Newslist = JsonConvert.DeserializeObject<List<NewsViewModelV2>>(data);
			}
			return View(Newslist);
		}

		[HttpGet] // Default
		public IActionResult Create()
		{
            IEnumerable<AuthorViewModel> Authorlist = new List<AuthorViewModel>();
            HttpResponseMessage response = _Client.GetAsync(baseAdress +
                "/Author/GetAllAuthors").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Authorlist = JsonConvert.DeserializeObject<IEnumerable<AuthorViewModel>>(data);
				
            }
			TempData["data"] = Authorlist;
            return View();
		}

		[HttpPost]
		public IActionResult Create(NewsViewModelV3 newsViewModel)
		{
            newsViewModel.Image = DocumentSettings.UploadFile(newsViewModel.ImageName, "images"); // Image

            string data = JsonConvert.SerializeObject(newsViewModel);
			StringContent content = new StringContent(data, Encoding.UTF8,
				"application/json");
			HttpResponseMessage response = _Client.PostAsync(baseAdress +
				$"/News/CreateNews/Create", content).Result;

			if (response.IsSuccessStatusCode)
			{
				//var resut = response.RequestMessage.Properties;
				return RedirectToAction(nameof(Index));

			}

			return View(newsViewModel);
		}

        [HttpGet] // Default
        public IActionResult Details(int? Id)
        {
            if (Id == null) return BadRequest();
            NewsViewModel Authorlist = new NewsViewModel();
            HttpResponseMessage response = _Client.GetAsync(baseAdress +
                $"/News/GetById/{Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Authorlist = JsonConvert.DeserializeObject<NewsViewModel>(data);
                return View(Authorlist);
            }
            return View();

        }

        [HttpGet] // Default
        public IActionResult Update(int? id)
        {
            if (id == null) return BadRequest();
            NewsViewModelV4 newsViewModel = new NewsViewModelV4();
            
            IEnumerable<AuthorViewModel> Authorlist = new List<AuthorViewModel>();
            HttpResponseMessage response = _Client.GetAsync(baseAdress +
                "/Author/GetAllAuthors").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Authorlist = JsonConvert.DeserializeObject<IEnumerable<AuthorViewModel>>(data);
            }

            HttpResponseMessage response1 = _Client.GetAsync(baseAdress +
               $"/News/GetById/{id}").Result;

            if (response1.IsSuccessStatusCode)
            {
                string data1 = response1.Content.ReadAsStringAsync().Result;
                newsViewModel = JsonConvert.DeserializeObject<NewsViewModelV4>(data1);
                }
            TempData["data"] = Authorlist;
            return View(newsViewModel);
        }


        [HttpPost]
        public IActionResult Update(NewsViewModelV4 newsViewModel)
        {
            newsViewModel.Image = DocumentSettings.UploadFile(newsViewModel.ImageName, "images");
            
            string data = JsonConvert.SerializeObject(newsViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = _Client.PostAsync(baseAdress +
                $"/News/UpdateNews/Update", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(newsViewModel);
        }


        [HttpGet] 
        public IActionResult Delete(int? Id)
        {
            if (Id == null) return BadRequest();
            NewsViewModel newsViewModel  = new NewsViewModel();
            HttpResponseMessage response = _Client.GetAsync(baseAdress +
                $"/News/GetById/{Id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                newsViewModel = JsonConvert.DeserializeObject<NewsViewModel>(data);
                return View(newsViewModel);
            }
            return View();

        }

        [HttpPost]
        public IActionResult Delete(NewsViewModel newsViewModel)
        {
            HttpResponseMessage response = _Client.DeleteAsync(baseAdress +
                $"/News/DeleteNews/{newsViewModel.Id}").Result;

            if (response.IsSuccessStatusCode)
            {
                //DocumentSettings.DeleteFile(newsViewModel.Image, "images");
                return RedirectToAction(nameof(Index));

            }

            return View(newsViewModel);
        }

    }
}
