using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.ManagerService
{
    public interface IManagerService
    {
        Task CreatePersonel(AddPersonelDTO addPersonelDTO);
        Task<List<ListOfPersonelVM>> GetPersonels();
        Task<UpdatePersonelDTO> GetPersonel(Guid id);
        Task UpdatePersonel(UpdatePersonelDTO updatePersonelDTO);
        Task DeletePersonel(Guid id);

    }
}
