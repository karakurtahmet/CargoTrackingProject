using BussinesLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BussinesLayer.ValidationRules;
using FluentValidation.Results;

namespace CargoTracking.Controllers
{
    public class AdminFirmController : Controller
    {
        FirmManager fm = new FirmManager(new EfFirmDal());
        FirmValidator firmvalidator = new FirmValidator();
        [Authorize]
        public ActionResult Index()
        {
            var firmvalues = fm.GetList();
            return View(firmvalues);
        }
        [HttpGet]
        [Authorize]
        public ActionResult AddFirm()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddFirm(Firm p)
        {
            ValidationResult result = firmvalidator.Validate(p);
            if (result.IsValid)
            {
                fm.FirmAdd(p);
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
        public ActionResult EditFirm(int id)
        {
            var firmvalues =fm.GetByID(id);
            return View(firmvalues);
        }
        [HttpPost]
        [Authorize]
        public ActionResult EditFirm(Firm f)
        {
            ValidationResult result = firmvalidator.Validate(f);
            if (result.IsValid)
            {
                fm.FirmUpdate(f);
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

        public ActionResult DeleteFirm(int id) 
        {
            var firmvalues = fm.GetByID(id);
            fm.FirmDelete(firmvalues);


            return RedirectToAction("Index");
        }

    }
}