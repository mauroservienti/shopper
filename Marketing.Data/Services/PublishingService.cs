using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketing.Data.Services
{
    public class PublishingService
    {
        public Task<dynamic> GetHomeShowcase()
        {
            dynamic vm = new ExpandoObject();
            vm.HeadlineStockItemId = 1;
            vm.ShowcaseStockItemIds = new[] { 2, 4, 5 };

            return Task.FromResult<dynamic>( vm );
        }
    }
}
