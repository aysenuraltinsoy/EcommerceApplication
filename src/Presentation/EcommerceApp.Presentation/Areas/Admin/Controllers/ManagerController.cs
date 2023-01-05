using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Services.AdminService;
using Microsoft.AspNetCore.Mvc;

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
            await _adminService.CreateManager(addManagerDTO);
            return RedirectToAction(nameof(ListOfManagers));
        }

        public async Task<IActionResult> ListOfManagers()
        {
            var managers=await _adminService.GetManagers();
            return View(managers);  
        }
    }
}
