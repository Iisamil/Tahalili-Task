using Microsoft.AspNetCore.Mvc;
using News.MVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace News.MVC.Controllers
{
    public class UserController : Controller
    {
        Uri baseAdress = new Uri("https://localhost:7200/api");
        private readonly HttpClient _Client;
        public UserController()
        {
            _Client = new HttpClient();
            _Client.BaseAddress = baseAdress;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel userViewModel)
        {
            string token;
            string data = JsonConvert.SerializeObject(userViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = _Client.PostAsync(baseAdress +
                "/User/Login/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                //string data1 = response.Content.().Result;
                //token = JsonConvert.DeserializeObject<string>(data1);
                return RedirectToAction("index","Home");
            }
            return View();
        }

    }
}
