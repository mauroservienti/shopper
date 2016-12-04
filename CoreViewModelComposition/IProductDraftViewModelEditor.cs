using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace CoreViewModelComposition
{
    public interface IProductDraftViewModelEditor
    {
        Task EditOne( string draftId, IDictionary<string, StringValues> form);
    }
}
