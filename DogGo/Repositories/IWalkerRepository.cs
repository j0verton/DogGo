using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();
        Walker GetWalkerByEmail(string email);
        Walker GetWalkerById(int id);

        List<Walker> GetWalkersInNeighborhood(int neighborhoodId);
    }
}

