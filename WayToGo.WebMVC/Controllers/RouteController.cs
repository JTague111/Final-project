using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WayToGo.Models;
using WayToGo.Models.Routes;
using WayToGo.Services;

namespace WayToGo.WebMVC.Controllers
{
    public class RouteController : Controller
    {
        // GET: Route
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RouteService(userId);
            var model = service.GetRoutes();

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
        public ActionResult Create(RouteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateRouteService();

            if (service.CreateRoute(model))
            {
                TempData["SaveResult"] = "Your route was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Route could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateRouteService();
            var model = svc.GetRouteById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateRouteService();
            var detail = service.GetRouteById(id);
            var model =
                new RouteEdit
                {
                    RouteId = detail.RouteId,
                    Name = detail.Name,
                    Origin = detail.Origin,
                    Destination = detail.Destination
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RouteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.RouteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateRouteService();

            if (service.UpdateRoute(model))
            {
                TempData["SaveResult"] = "Your route was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your route could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateRouteService();
            var model = svc.GetRouteById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateRouteService();

            service.DeleteRoute(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private RouteService CreateRouteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new RouteService(userId);
            return service;
        }
    }
}