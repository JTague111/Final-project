using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WayToGo.Models.Transports;
using WayToGo.Services;

namespace WayToGo.WebMVC.Controllers
{
    public class TransportController : Controller
    {
        // GET: Transport
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransportService(userId);
            var model = service.GetTransports();

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
        public ActionResult Create(TransportCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTransportService();

            if (service.CreateTransport(model))
            {
                TempData["SaveResult"] = "Your transport was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Transport could not be created");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateTransportService();
            var model = svc.GetTransportById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateTransportService();
            var detail = service.GetTransportById(id);
            var model =
                new TransportEdit
                {
                    TransportId = detail.TransportId,
                    Name = detail.Name,
                    Description = detail.Description,
                    Speed = detail.Speed,
                    Cost = detail.Cost
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TransportEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TransportId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateTransportService();

            if (service.UpdateTransport(model))
            {
                TempData["SaveResult"] = "Your transport was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your transport could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTransportService();
            var model = svc.GetTransportById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateTransportService();

            service.DeleteTransport(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private TransportService CreateTransportService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TransportService(userId);
            return service;
        }
    }
}