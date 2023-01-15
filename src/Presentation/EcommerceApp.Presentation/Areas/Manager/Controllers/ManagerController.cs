using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Application.Services.ManagerService;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Presentation.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ManagerController : Controller
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService=managerService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddPersonel()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddPersonel(AddPersonelDTO addPersonelDTO)
        {
            if (ModelState.IsValid)
            {
                await _managerService.CreatePersonel(addPersonelDTO);
                return RedirectToAction(nameof(ListOfPersonel));
            }
            return View();

        }

        public async Task<IActionResult> ListOfPersonel()
        {
            var personels = await _managerService.GetPersonels();
            return View(personels);
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePersonel(Guid id)
        {
            var updatePersonel = await _managerService.GetPersonel(id);
            return View(updatePersonel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePersonel(UpdatePersonelDTO updatePersonelDTO)
        {
            if (ModelState.IsValid)
            {
                await _managerService.UpdatePersonel(updatePersonelDTO);
                return RedirectToAction(nameof(ListOfPersonel));

            }
            return View(updatePersonelDTO);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteManager(Guid id)
        {
            await _managerService.DeletePersonel(id);
            return RedirectToAction(nameof(ListOfPersonel));
        }
    }
}
