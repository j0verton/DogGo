using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class BookWalkerViewModel
    {
        Walker Walker { get; set; }
        List<Walk> CurrentWalks { get; set; }

        Owner CurrentOwner { get; set; }

       List<Dog> OwnerDogs { get; set; }
    }
}
