using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WayToGo.Models;
using WayToGo.Services;

namespace WayToGo.WebMVC.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CityService(userId);
            var model = service.GetCities();

            return View(model);
        }
        //Add method here VVVV
        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateCityService();

            if (service.CreateCity(model))
            {
                TempData["SaveResult"] = "Your city was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "City could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateCityService();
            var model = svc.GetCityById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateCityService();
            var detail = service.GetCityById(id);
            var model =
                new CityEdit
                {
                    CityId = detail.CityId,
                    Name = detail.Name,
                    Latitude = detail.Latitude,
                    Longitude = detail.Longitude
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.CityId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCityService();

            if (service.UpdateCity(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateCityService();
            var model = svc.GetCityById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCityService();

            service.DeleteCity(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private CityService CreateCityService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CityService(userId);
            return service;
        }
    }
}