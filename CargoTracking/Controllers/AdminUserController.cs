using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccessLayer;
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
    public class AdminUserController : Controller
    {
        UserManager um = new UserManager(new EfUserDal());
        FirmManager fm = new FirmManager(new EfFirmDal());
        UserValidator uservalidator = new UserValidator();

        // GET: AdminUser
        [Authorize]
        public ActionResult Index()
        {
            var uservalues = um.GetList();
            return View(uservalues);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddUser()
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
        public ActionResult AddUser(User u)
        {
            ValidationResult result = uservalidator.Validate(u);
         
            if (result.IsValid)
            {
                um.UserAdd(u);
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
        public ActionResult EditUser(int id)
        {
            List<SelectListItem> firmvalues = (from x in fm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.FirmName,
                                                   Value = x.FirmId.ToString(),
                                               }).ToList();
            ViewBag.vlf = firmvalues;


            var uservalues = um.GetByID(id);
            return View(uservalues);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditUser(User u)
        {

            



            ValidationResult result = uservalidator.Validate(u);
            if (result.IsValid)
            {
                um.UserUpdate(u);
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

        public ActionResult DeleteUser(int id)
        {
            var uservalues = um.GetByID(id);
            um.UserDelete(uservalues);
            return RedirectToAction("Index");
        }
    }
}