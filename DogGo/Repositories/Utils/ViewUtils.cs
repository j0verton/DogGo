using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories.Utils
{
    public class ViewUtils
    {
        public static Dog getDogFromListById(List<Dog> dogs, int dogId)
        {
            Dog foundDog = dogs.FirstOrDefault(dog => dog.Id == dogId);
            return foundDog;
        }

        public static Owner getOwnerFromListByDog(List<Owner> owners, Dog dog)
        {
            return owners.FirstOrDefault(owner => owner.Id == dog.OwnerId);
        }
    }
}
