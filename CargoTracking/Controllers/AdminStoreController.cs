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
    public class AdminStoreController : Controller
    {
        StoreManager sm = new StoreManager(new EfStoreDal());
        FirmManager fm = new FirmManager(new EfFirmDal());
        StoreValidator storevalidator = new StoreValidator();
        // GET: AdminStore
        [Authorize]
        public ActionResult Index()
        {
            var storevalues = sm.GetList();
            return View(storevalues);
        }

        
        [HttpGet]
        [Authorize]
        public ActionResult AddStore()
        {
            List<SelectListItem> firmvalues = (from x in fm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.FirmName,
                                                   Value = x.FirmId.ToString(),
                                               }).ToList();
            ViewBag.vlf = firmvalues;
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddStore(Store s)
        {
            ValidationResult result = storevalidator.Validate(s);



            if (result.IsValid)
            {
                sm.StoreAdd(s);
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
        public ActionResult EditStore(int id)
        {

            List<SelectListItem> firmvalues = (from x in fm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.FirmName,
                                                   Value = x.FirmId.ToString(),
                                               }).ToList();
            ViewBag.vlf = firmvalues;


            var storevalues = sm.GetByID(id);
            return View(storevalues);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditStore(Store s)
        {
    

            ValidationResult result = storevalidator.Validate(s);
            if(result.IsValid)
            {
                sm.StoreUpdate(s);
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

        public ActionResult DeleteStore(int id)
        {
            var storevalues = sm.GetByID(id);
            sm.StoreDelete(storevalues);
            return RedirectToAction("Index");
        }

    }
}