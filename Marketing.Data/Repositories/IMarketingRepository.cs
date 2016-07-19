using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marketing.Data.Models;

namespace Marketing.Data.Repositories
{
    public interface IMarketingRepository
    {
        Task<Price> Price(Guid productId);
        Task<List<Price>> Prices();
    }
}