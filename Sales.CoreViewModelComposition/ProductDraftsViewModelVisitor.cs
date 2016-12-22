using CoreViewModelComposition;
using HttpHelpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sales.CoreViewModelComposition
{
    public class ProductDraftsViewModelVisitor : IProductDraftsViewModelVisitor
    {
        readonly IConfiguration _config;

        public ProductDraftsViewModelVisitor(IConfiguration config)
        {
            _config = config;
        }

        public Task VisitAll(IEnumerable<dynamic> composedViewModels)
        {
            return Task.CompletedTask;
        }

        public Task VisitEditableOne(dynamic composedViewModel)
        {
            dynamic proposedPrice = new ExpandoObject();
            proposedPrice.StockItemId = composedViewModel.StockItemId;
            proposedPrice.Price = 0;

            composedViewModel.ProposedPrice = proposedPrice;

            return Task.CompletedTask;
        }
    }
}
