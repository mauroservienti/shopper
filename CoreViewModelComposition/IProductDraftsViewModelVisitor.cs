using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreViewModelComposition
{
    public interface IProductDraftsViewModelVisitor
    {
        Task VisitAll(IEnumerable<dynamic> composedViewModels);
        Task VisitEditableOne(dynamic composedViewModel);
    }
}
