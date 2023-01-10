using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.AdminService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace EcommerceApp.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagerController : Controller
    {

        private readonly IAdminService _adminService;
        public ManagerController(IAdminService adminService)
        {
            _adminService=adminService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task <IActionResult> AddManager()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddManager(AddManagerDTO addManagerDTO)
        {
            //if (ModelState.IsValid)
            //{
            //    await _adminService.CreateManager(addManagerDTO);
            //    return RedirectToAction(nameof(ListOfManagers));
            //}
            //return View();
            
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/");
                
                var responseTask = client.PostAsJsonAsync<AddManagerDTO>("api/Manager/PostManager",addManagerDTO);
                responseTask.Wait();
                var resultTask=responseTask.Result;
                if (responseTask.IsCompletedSuccessfully)
                {
                   return RedirectToAction(nameof(ListOfManagers));
                }
                else
                {
                    return BadRequest();
                }
            }
            
        }

        public async Task<IActionResult> ListOfManagers()
        {
            //var managers=await _adminService.GetManagers();
            //return View(managers);  
            using (var client = new HttpClient()) 
            {
                client.BaseAddress = new Uri("https://localhost:44349/"); //api's server address
                var responseTask = client.GetAsync("api/Manager/GetManagers");
                responseTask.Wait();
                var resultTask=responseTask.Result;  //return json type result
                if (responseTask.IsCompletedSuccessfully)
                {
                    var readTask = resultTask.Content.ReadAsStringAsync();
                    readTask.Wait();

                    var readData = JsonConvert.DeserializeObject<List<ListOfManagerVM>>(readTask.Result);

                    return View(readData);
                }
                else
                {
                    ViewBag.EmptyList = "List is not found.";
                    return View(new List<ListOfManagerVM>());
                }
            };
        }

        [HttpGet]
        public async Task<IActionResult> UpdateManager(Guid id)
        {
            var updateManager = await _adminService.GetManager(id);
            return View(updateManager);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateManager(UpdateManagerDTO updateManagerDTO)
        {
            if (ModelState.IsValid)
            {
                await _adminService.UpdateManager(updateManagerDTO);
                return RedirectToAction(nameof(ListOfManagers));

            }
            return View(updateManagerDTO);
        }
        public async Task<IActionResult> DeleteManager(Guid id)
        {
            await _adminService.DeleteManager(id);
            return RedirectToAction(nameof(ListOfManagers));
        }
    }
}
