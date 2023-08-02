using BussinesLayer.Concrete;
using DataAccessLayer;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CargoTracking.Controllers
{
    public class AccountController : Controller
    {
        UserManager um = new UserManager(new EfUserDal());

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        //Post Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
             
                // Authenticate user
                if (AuthenticateUser(model.UserName, model.Password))
                {
                    if (model.UserName == "admin")
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);

                        return RedirectToAction("Index", "Admin"); 
                    }
                    else if (model.UserName == null)
                    {
                        ModelState.AddModelError("","Invalid username or password");
                        return View(ModelState);
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        
                        return RedirectToAction("Index", "User");
                    }
                }
                else
                {
                    return View();
                }
            }


            return View();
        }

    
        private bool AuthenticateUser(string username, string password)
        {
            
            
            var user = um.GetList().FirstOrDefault(u => u.UserName == username && u.Password == password);

            return user != null;
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}