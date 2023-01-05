using EcommerceApp.Application.Extensions;
using EcommerceApp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Models.DTOs
{
    public class AddManagerDTO
    {
        public Guid Id { get; set; }=Guid.NewGuid();
        [Required(ErrorMessage="Can not be empty!")]
        [MaxLength(25,ErrorMessage ="You can not enter more then 25 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Can not be empty!")]
        [MaxLength(50, ErrorMessage = "You can not enter more then 50 characters")]
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }=DateTime.Now;

        public Status Status { get; set; } = Status.Active;
        [BirthDateExtension(ErrorMessage = "The employee must be over 18 years old.")]

        public DateTime BirthDate { get; set; }
        public Roles Roles { get; set; } = Roles.Manager;
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; }
        [PictureFileExtension]
        public IFormFile UploadPath { get; set; }
    }
}
