using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        void ConfirmWalkRequest(int id);
        List<Walk> GetWalksByWalkerId(int id);
        void RequestWalk(Walk walk);

    }
}