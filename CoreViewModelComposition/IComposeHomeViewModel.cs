﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreViewModelComposition
{
    public interface IComposeHomeViewModel
    {
        Task Compose(dynamic composedViewModel);
    }
}
