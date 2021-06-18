using core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.ViewModels
{
    public class HomeViewModel
    {
        public Owner owner { get; set; }
        public List<portfolioItem> portfolioItems { get; set; }
    }
}
