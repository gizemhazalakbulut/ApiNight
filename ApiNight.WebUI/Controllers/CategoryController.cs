using ApiNight.WebUI.Dtos.CategoryDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
            if(responseMessage.IsSuccessStatusCode) // Eğer başarılı bir şekilde sonuç dönerse
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Json veriyi string olarak alıyoruz.
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // Json veriyi ResultCategoryDto tipine deserialize ediyoruz.
                return View(values); // Veriyi view a gönderiyoruz.
            }
         
            return View(); // Eğer başarılı bir sonuç dönmezse boş bir view döndürüyoruz.
        }
    }
}
