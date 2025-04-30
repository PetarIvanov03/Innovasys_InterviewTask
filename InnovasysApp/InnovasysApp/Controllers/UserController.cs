using InnovasysApp.Repositories;
using InnovasysApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InnovasysApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadData()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserViewModel>>(json);

            return View("Index", users);
        }


        [HttpPost]
        public async Task<IActionResult> SaveAll(List<UserViewModel> users, [FromServices] UserRepository repo)
        {
            if (users == null || !users.Any())
            {
                TempData["Success"] = "Няма подадени потребители за запис.";
                return RedirectToAction("Index");
            }

            try
            {
                await repo.SaveUsersAsync(users);
                TempData["Success"] = "Данните бяха успешно записани в базата.";
            }
            catch (Exception ex)
            {
                TempData["Success"] = "Възникна грешка при запис: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> All([FromServices] UserRepository repo)
        {
            var users = await repo.GetAllUsersAsync();
            return View(users);
        }

    }
}
