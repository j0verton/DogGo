using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repositories
{
    public class IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogsByOwnerById(int id);
        void UpdateDog(Dog dog);
        void DeleteDog(int dogId);
        void AddDog(Dog dog);
    }
}
