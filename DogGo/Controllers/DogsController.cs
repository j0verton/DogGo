﻿using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
        [Authorize]
    public class DogsController : Controller
    {
        private readonly IDogRepository _dogRepo;
        private readonly IWalkRepository _walkRepo;

        public DogsController(IDogRepository dogRepository, IWalkRepository walkRepository)
        {
            _dogRepo = dogRepository;
            _walkRepo = walkRepository;

        }
        // GET: OwnerController
        // /dog/index
        // uncomment this v when ready , can also move it above class to lock down all routes
        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            return View(dogs);
        }


        // GET: DogController1/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            List<Walk> walks = _walkRepo.GetWalksByDogId(id);
            int currentUserId = GetCurrentUserId();
            if (dog.OwnerId != currentUserId)
            {
                return NotFound();
            }
            if (dog == null)
            {
                return NotFound();
            }

            DogProfileViewModel vm = new DogProfileViewModel()
            {
                Dog = dog,
                Walks = walks

            };
            return View(vm);
        }

        // GET: DogController1/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController1/Edit/5
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }
            int currentUserId = GetCurrentUserId();

            if (currentUserId != dog.OwnerId)
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                _dogRepo.UpdateDog(dog);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController1/Delete/5
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);
            int currentUserId = GetCurrentUserId();

            if (currentUserId != dog.OwnerId)
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
