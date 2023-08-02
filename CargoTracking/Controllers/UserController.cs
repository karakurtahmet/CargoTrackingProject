using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using CargoTracking.Models;
using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CargoTracking.Controllers
{
    public class UserController : Controller
    {
        PackageTrackingManager ptm = new PackageTrackingManager(new EfPackageTracking());
        UserManager um = new UserManager(new EfUserDal());
        StoreManager sm = new StoreManager(new EfStoreDal());
        PackageManager pm = new PackageManager(new EfPackageDal());
        DemandValidator demandvalidator = new DemandValidator();
        DemandManager dm = new DemandManager(new EfDemandDal());
        DemandStatusManager dsm = new DemandStatusManager(new EfDemandStatusDal());
        DemandTrackingManager dtm = new DemandTrackingManager(new EfDemandTrackingDal());
        // GET: User
        [Authorize]
        public ActionResult Index()
        {

            ViewBag.User = User.Identity.Name;
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult CreateDemand()
        {

            var currentuser = User.Identity.Name;
            var user = um.GetByUsername(currentuser);
            List<SelectListItem> storevalues = (from x in sm.GetList().Where(x => x.FirmId == user.FirmId)
                                                select new SelectListItem
                                                {
                                                    Text = x.StoreName,
                                                    Value = x.StoreCode,
                                                }).ToList();


            ViewBag.storevalues = storevalues;
            ViewBag.StorePackage = GetPackageCountsForStores();


            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult CreateDemandResult(Demand d)
        {
            d.DemandDate = DateTime.Now;
            d.StatusId = 1;
            d.RejectionReason = null;
            var user = um.GetByUsername(User.Identity.Name);
            d.UserId = user.UserId;
            var storePackage = GetPackageCountsForStores();
            var PackageNumber = storePackage[d.StoreCode];

            ValidationResult result = demandvalidator.Validate(d);
            if (result.IsValid)
            {
                if (d.Quantity > PackageNumber)
                {
                    ModelState.AddModelError("", "Talep kolisi mevcut koliden fazla");
                    return RedirectToAction("CreateDemand");
                }

                dm.DemandAdd(d);
               
                
                //creating log
                DemandTracking demandTracking = new DemandTracking
                {
                    DemandId = d.DemandId,
                    UserId = d.UserId,
                    DemandStatusId = d.StatusId,
                    IndexDate = DateTime.Now,
                    ChangeDescription = dsm.GetById(d.StatusId).DemandStatusName
                };
                dtm.DemandTrackingAdd(demandTracking);

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
        [HttpGet]
        public ActionResult DemandTracking()
        {
            var currentUser = User.Identity.Name;
            var user = um.GetByUsername(currentUser);
            var demandtrackingvalues = dtm.GetList().Where(x => x.UserId == user.UserId).ToList();

            return View(demandtrackingvalues);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DemandTracking(int demandId)
        {
            var currentuser = User.Identity.Name;
            var user = um.GetByUsername(currentuser);
            var demandtrackingvalues = dtm.GetList().Where(x=>x.UserId == user.UserId).ToList();
            var idsearch = demandtrackingvalues.Where(x=>x.DemandId == demandId).ToList();
            return View(idsearch);
        }




        [Authorize]
        [HttpGet]
        public ActionResult DemandsList()
        {
            var currentuser = User.Identity.Name;
            var user = um.GetByUsername(currentuser);
            var demandvalues = dm.GetList().Where(x => x.UserId == user.UserId).ToList();

            return View(demandvalues);
        }

        public ActionResult CancelDemand(int id)
        {
            var demandvalues = dm.GetById(id);
            if (demandvalues == null)
            {
                dtm.DemandTrackingDelete(dtm.GetById(id));
                return RedirectToAction("Index");
            }
            else
            {
                demandvalues.StatusId = 2;
               

                DemandTracking demandTracking = new DemandTracking
                {
                    DemandId = demandvalues.DemandId,
                    UserId = demandvalues.UserId,
                    DemandStatusId = demandvalues.StatusId,
                    IndexDate = DateTime.Now
                };
                string changeDescription = GenerateChangeDescription(1, demandvalues.StatusId);
                demandTracking.ChangeDescription = changeDescription;
                dtm.DemandTrackingAdd(demandTracking);


                dm.DemandUpdate(demandvalues);
                return RedirectToAction("DemandsList");
            }

        }
        [Authorize]
        public ActionResult GetPackagesByDemandId(int id)
        {
            var values = pm.GetList().Where(x=>x.DemandID == id).ToList(); //pm = package manager
            return View(values);
        }

        [Authorize]
        [HttpGet]
        public ActionResult PackageTracking()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult PackageTrackingResult(BarcodeViewModel model)
        {
            List<PackageTracking> packageTrackings = ptm.GetByBarcode(model.BarcodeNumber);
            ViewBag.PackageTracking = model.BarcodeNumber;
            return View(packageTrackings);

        }



        private Dictionary<string, int> GetPackageCountsForStores()
        {
            var stores = sm.GetList();
            var packageCounts = new Dictionary<string, int>();

            foreach (var store in stores)
            {
                int packageCount = pm.GetList().Count(p => p.StoreCode == store.StoreCode);
                packageCounts.Add(store.StoreCode, packageCount);
            }

            return packageCounts;
        }
        private string GenerateChangeDescription(int oldStatusId, int newStatusId)
        {
            Dictionary<int, string> statusDescriptions = GetStatusDescriptionsFromDatabase();

            string oldStatusDescription = statusDescriptions.TryGetValue(oldStatusId, out string oldDescription) ? oldDescription : "Unknown";
            string newStatusDescription = statusDescriptions.TryGetValue(newStatusId, out string newDescription) ? newDescription : "Unknown";

            return $"Talebin durumu '{oldStatusDescription}' 'dan '{newStatusDescription}' 'a geçmiştir.";
        }
        private Dictionary<int, string> GetStatusDescriptionsFromDatabase()
        {
            Dictionary<int, string> statusDescriptions = new Dictionary<int, string>();

            // Fetch demand status descriptions from the database
            var demandStatuses = dsm.GetList();

            foreach (var demandStatus in demandStatuses)
            {
                statusDescriptions.Add(demandStatus.DemandStatusId, demandStatus.DemandStatusName);
            }

            return statusDescriptions;
        }

    }
}