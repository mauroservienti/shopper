using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelComposition
{
    public interface IComposeHomeViewModel
    {
        Task Compose(dynamic composedViewModel);
    }
}
