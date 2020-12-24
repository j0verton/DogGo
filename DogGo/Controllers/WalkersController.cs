using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private IWalkerRepository _walkerRepo;
        private IWalkRepository _walkRepo;
        private INeighborhoodRepository _neighborhoodRepo;
        private IOwnerRepository _ownerRepo;
        private IDogRepository _dogRepo;
        public WalkersController(IWalkerRepository walkerRepository, IWalkRepository walkRepository, INeighborhoodRepository neighborhoodrepo, IOwnerRepository ownerRepository, IDogRepository dogRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _neighborhoodRepo = neighborhoodrepo;
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
        }

        // GET: WalkersController
        public ActionResult Index()
        {
            int currentUserId = GetCurrentUserId();
            Owner currentOwner = _ownerRepo.GetOwnerById(currentUserId);

            List<Walker> walkers = _walkerRepo.GetAllWalkers();
            
            return View(walkers.Where(walker => walker.NeighborhoodId == currentOwner.NeighborhoodId));
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walk> walks = _walkRepo.GetWalksByWalkerId(walker.Id);
            Neighborhood neighborhood = _neighborhoodRepo.GetNeighborhoodById(walker.NeighborhoodId);
            List<Owner> clientOwners = _ownerRepo.GetOwnersByEmployedWalkerId(walker.Id);
            List<Dog> clientDogs = _dogRepo.GetDogsByEmployedWalkerId(walker.Id);

            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks,
                Neighborhood = neighborhood,
                WalkSummaries =new List<WalkSummaryViewModel>(),
                clientOwners = clientOwners,
                clientDogs = clientDogs
            };
                foreach (Walk walk in vm.Walks)
                {
                    vm.WalkSummaries.Add(new WalkSummaryViewModel()
                    { 
                        walk = walk
                    });
                }

            if (vm.Walker == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        // 
        public ActionResult Book(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            return View(vm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
