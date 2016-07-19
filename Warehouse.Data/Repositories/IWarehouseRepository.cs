using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Data.Models;

namespace Warehouse.Data.Repositories
{
    public interface IWarehouseRepository
    {
        Task<Customer> Customer(Guid id);
        Task<List<Customer>> Customers();
    }
}