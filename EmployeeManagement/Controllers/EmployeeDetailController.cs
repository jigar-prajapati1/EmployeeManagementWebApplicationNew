using CommonModels.DbModels;
using CommonModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace EmployeeManagement.Controllers
{
    public class EmployeeDetailController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IWebHostEnvironment _hostEnvironment;
        public EmployeeDetailController(IHttpClientFactory httpClientFactory, IWebHostEnvironment hostEnvironment)
        {
            this.httpClientFactory = httpClientFactory;
            this._hostEnvironment = hostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<EmployeeDetailView> response = new List<EmployeeDetailView>();
            try
            {
                //Get All EmployeeDetail from WebApi
                var client = httpClientFactory.CreateClient();
                var apiUrl= "https://localhost:7296/api/EmployeeDetail";
                var httpResponseMessage = await client.GetAsync( apiUrl);
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<EmployeeDetailView>>());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployeeDetail(EmployeeDetailView employee)
        {
            try
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (employee.ImageFile != null && employee.ImageFile.FileName != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                    string extension = Path.GetExtension(employee.ImageFile.FileName);
                    employee.ProfilePicture = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "Images", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await employee.ImageFile.CopyToAsync(fileStream);
                    }
                }

                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7296/api/EmployeeDetail"),
                    Content = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<EmployeeDetailView>();
                if (response != null)
                {
                    return RedirectToAction("Index", employee);
                }
                return View();
            }
            catch(Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var response = await client.GetFromJsonAsync<EmployeeDetailView>($"https://localhost:7296/api/EmployeeDetail/{id.ToString()}");
                if (response != null)
                {
                    return View(response);
                }
                return View(null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeDetailView employee)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri($"https://localhost:7296/api/EmployeeDetail/{employee.Id}"),
                    Content = new StringContent(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadFromJsonAsync<EmployeeDetailView>();
                if (httpResponseMessage != null)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult>DeleteEmployee(EmployeeDetailView employee)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.DeleteAsync($"https://localhost:7296/api/EmployeeDetail/{employee.Id}");
                httpResponseMessage.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "EmployeeDetail");
            }
            catch(Exception ex)
            {
                return BadRequest($"Could not delete {ex.Message}");
            }
        }
    } 
}
