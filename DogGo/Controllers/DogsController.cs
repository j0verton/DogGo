using DogGo.Models;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class DogsController : Controller
    {
        private readonly IDogRepository _dogRepo;

        public DogsController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }
        // GET: OwnerController
        public ActionResult Index()
        {

            List<Dog> dogs = _dogRepo.GetAllDogs();
            return View(dogs);

        }


        // GET: DogController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DogController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DogController1/Create
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

        // GET: DogController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DogController1/Edit/5
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

        // GET: DogController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DogController1/Delete/5
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
