using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class BookWalkerViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> CurrentWalks { get; set; }

        public Owner CurrentOwner { get; set; }

        public List<Dog> OwnerDogs { get; set; }
    }
}
