using ApiNight.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiNight.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> CategoryList()
        {
            var client = _httpClientFactory.CreateClient(); // Api ye sorgu yapmamızı sağlayan nesne oluşturuyor.
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Categories"); // Api ye sorgu atıyoruz. İstekte bulunuyoruz.
            if (responseMessage.IsSuccessStatusCode) // Eğer başarılı bir şekilde sonuç dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Json veriyi string olarak alıyoruz.
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // Json veriyi ResultCategoryDto tipine deserialize ediyoruz.
                return View(values); // Veriyi view a gönderiyoruz.
            }

            return View(); // Eğer başarılı bir sonuç dönmezse boş bir view döndürüyoruz.
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(); // Kategori oluşturma sayfasını döndürüyoruz.
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PostAsync("https://localhost:7196/api/Categories", stringContent);
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync("https://localhost:7196/api/Categories?id=" + id);
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7196/api/Categories/GetCategory?id=" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<GetCategoryByIdDto>(jsonData);
            return View(values); // Kategori güncelleme sayfasını döndürüyoruz.
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            await client.PutAsync("https://localhost:7196/api/Categories", stringContent);
            return RedirectToAction("CategoryList"); 
        }
    }
}
