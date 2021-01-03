using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalksController : DogGoControllerBase
    {

        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalksController(IWalkerRepository walkerRepository, IWalkRepository walkRepository, IOwnerRepository ownerRepository, IDogRepository dogRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
            _dogRepo = dogRepository;
        }
        // GET: WalksController
        public ActionResult Index()
        {

            int ownerId = GetCurrentUserId();

            List<Walk> walks = _walkRepo.GetWalksByWalkerId(ownerId);

            return View(walks);
            
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WalksController/Create
        public ActionResult Create(int id)
        {
            BookWalkerViewModel vm = new BookWalkerViewModel()
            {
                Walker = _walkerRepo.GetWalkerById(id),
                CurrentWalks = _walkRepo.GetWalksByWalkerId(id),
                CurrentOwner = _ownerRepo.GetOwnerById(GetCurrentUserId()),
                OwnerDogs = _dogRepo.GetDogsByOwnerId(GetCurrentUserId()),
                Walk = new Walk()
                {
                    Date = DateTime.Now,
                    WalkerId = _walkerRepo.GetWalkerById(id).Id,
                    WalkStatusId = 1
                },
            };

            return View(vm);
        }

        // POST: WalksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookWalkerViewModel vm)
        {
            try
            {

                _walkRepo.RequestWalk(vm.Walk);
                return RedirectToAction(nameof(Index), "Owners");
            }
            catch
            {
                BookWalkerViewModel reloadVM = vm;
                reloadVM.OwnerDogs = _dogRepo.GetDogsByOwnerId(GetCurrentUserId());
                return View(reloadVM);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
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

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
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
    }
}
