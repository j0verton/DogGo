using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }

        public Neighborhood Neighborhood { get; set; }

        public List<Owner> clientOwners { get; set; }

        public List<Dog> clientDogs { get; set; }

    }
}
