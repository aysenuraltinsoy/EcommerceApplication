﻿using Ecommerce.Infrastructure.Context;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class EmployeeRepo : BaseRepo<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(ECommerceAppDbContext eCommerceAppDbContext) : base(eCommerceAppDbContext)
        {
        }
    }
}
