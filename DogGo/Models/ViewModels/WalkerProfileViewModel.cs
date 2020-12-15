using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker walker { get; set; }
        public List<Walk> walks { get; set; }

        public Neighborhood neighborhood { get; set; }

    }
}
