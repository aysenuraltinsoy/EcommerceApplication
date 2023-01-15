using AutoMapper;
using EcommerceApp.Application.Models.DTOs;
using EcommerceApp.Application.Models.VMs;
using EcommerceApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Employee, AddManagerDTO>().ReverseMap();
            CreateMap<Employee, AddPersonelDTO>().ReverseMap();
            CreateMap<Employee, ListOfManagerVM>().ReverseMap();
            CreateMap<Employee, ListOfPersonelVM>().ReverseMap();
            CreateMap<UpdateManagerDTO, UpdateManagerVM>().ReverseMap();
            CreateMap<UpdateManagerDTO, Employee>().ReverseMap();
            CreateMap<UpdatePersonelDTO, Employee>().ReverseMap();
            CreateMap<UpdatePersonelDTO, Employee>().ReverseMap();
        }
    }
}
