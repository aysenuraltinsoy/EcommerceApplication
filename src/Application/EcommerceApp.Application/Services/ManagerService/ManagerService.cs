using AutoMapper;
using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using EcommerceApp.Domain.Enums;

namespace EcommerceApp.Application.Services.ManagerService
{
    public class ManagerService : IManagerService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepo _employeeRepo;
        public ManagerService(IMapper mapper, IEmployeeRepo employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }
        public async Task CreatePersonel(AddPersonelDTO addPersonelDTO)
        {
            var addPersonel = _mapper.Map<Employee>(addPersonelDTO);
            if (addPersonel.UploadPath!=null)
            {
                using var image= Image.Load(addPersonelDTO.UploadPath.OpenReadStream());
                image.Mutate(x=>x.Resize(600,560));

                Guid guid=Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                addPersonel.ImagePath = ($"/images/{guid}.jpg");
                await _employeeRepo.Create(addPersonel);
            }
            else
            {
                addPersonel.ImagePath = ($"/images/default.png");
                await _employeeRepo.Create(addPersonel);
            }
        }

        public async Task DeletePersonel(Guid id)
        {
            var model=await _employeeRepo.GetDefault(x=>x.Id==id);

            model.DeleteDate=DateTime.Now;
            model.Status = Domain.Enums.Status.Passive;
            await _employeeRepo.Delete(model);
        }

        public async Task<UpdatePersonelDTO> GetPersonel(Guid id)
        {
            var personel = await _employeeRepo.GetFilteredFirstOrDefault(
                select: x => new UpdatePersonelDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    ImagePath = x.ImagePath

                }, where: x => x.Id == id);
            var updatePersonelDTO = _mapper.Map<UpdatePersonelDTO>(personel);
            return updatePersonelDTO;
        }

        public async Task<List<ListOfPersonelVM>> GetPersonels()
        {
            var personels = await _employeeRepo.GetFilteredList(select: x => new ListOfPersonelVM
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Roles = x.Roles,
                ImagePath = x.ImagePath

            },
            where: x => ((x.Status == Status.Active || x.Status == Status.Modified) && x.Roles == Roles.Personel),
            orderBy: x => x.OrderBy(x => x.Name));

            return personels;
        }

        public async Task UpdatePersonel(UpdatePersonelDTO updatePersonelDTO)
        {
            var model = await _employeeRepo.GetDefault(x => x.Id == updatePersonelDTO.Id);
            model.Name = updatePersonelDTO.Name;
            model.Surname = updatePersonelDTO.Surname;

            model.UpdateDate = updatePersonelDTO.UpdateDate;
            model.Status = updatePersonelDTO.Status;
            using var image = Image.Load(updatePersonelDTO.UploadPath.OpenReadStream());
            image.Mutate(x => x.Resize(600, 560)); //picture size fixed

            Guid guid = Guid.NewGuid();
            image.Save($"wwwroot/images/{guid}.jpg");

            model.ImagePath = ($"/images/{guid}.jpg");
            await _employeeRepo.Update(model);
        }
    }
}
