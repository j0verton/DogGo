﻿using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public interface IWalkRepository
    {
        void CompleteWalk(int id, int duration);
        void ConfirmWalkRequest(int id);
        Walk GetWalkById(int walkId);
        List<Walk> GetWalksByWalkerId(int id);
        void RequestWalk(Walk walk);

    }
}