using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoTracking.Controllers
{
    public class AdminWarehouseController : Controller
    {
        WarehouseManager wm = new WarehouseManager(new EfWarehouseDal());
        WarehouseValidator validator = new WarehouseValidator();

        // GET: AdminWarehouse
        [Authorize]
        public ActionResult Index()
        {
            var warehousevalues = wm.GetList();
            return View(warehousevalues);
        }
        [HttpGet]
        [Authorize]
        public ActionResult AddWarehouse()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddWarehouse(Warehouse p)
        {
            ValidationResult result = validator.Validate(p);
            if (result.IsValid)
            {
                wm.WarehouseAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();
        }


        [HttpGet]
        [Authorize]
        public ActionResult EditWarehouse(int id)
        {
            var warehousevalues = wm.GetByID(id);
            return View(warehousevalues);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditWarehouse(Warehouse w)
        {
            ValidationResult result = validator.Validate(w);
            if (result.IsValid)
            {
                wm.WarehouseUpdate(w);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);

                }
            }
            return View();

        }

        [Authorize]
        public ActionResult DeleteWarehouse(int id)
        {
            var warehousevalues = wm.GetByID(id);
            wm.WarehouseDelete(warehousevalues);
            return RedirectToAction("Index");
        }
    }
}