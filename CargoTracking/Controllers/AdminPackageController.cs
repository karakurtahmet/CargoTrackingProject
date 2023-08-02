using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace CargoTracking.Controllers
{
    public class AdminPackageController : Controller
    {
        WarehouseManager wm = new WarehouseManager(new EfWarehouseDal());
        StoreManager sm = new StoreManager(new EfStoreDal());
        DemandManager dm = new DemandManager(new EfDemandDal());
        PackageManager pm = new PackageManager(new EfPackageDal());
        PackageStatusManager psm = new PackageStatusManager(new EfPsDal());
        PackageValidator packagevalidator = new PackageValidator();
        PackageTrackingManager ptm = new PackageTrackingManager(new EfPackageTracking());
        Context c = new Context();

        // GET: AdminPackage
        [Authorize]
        public ActionResult Index()
        {
            var packvalues=pm.GetList();
            return View(packvalues);
        }

        public Tuple<List<SelectListItem>, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>> GetValues()
        {
            List<SelectListItem> warehousevalues = (from x in wm.GetList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.WarehouseName,
                                                        Value = x.WarehouseId.ToString(),
                                                    }).ToList();
            List<SelectListItem> storevalues = (from x in sm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.StoreName,
                                                   Value = x.StoreCode,
                                               }).ToList();
            List<SelectListItem> demandvalues = (from x in dm.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.User.UserName,
                                                   Value = x.DemandId.ToString(),
                                               }).ToList();
            List<SelectListItem> psvalues = (from x in psm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.PStatusName,
                                                     Value = x.PStatusId.ToString(),
                                                 }).ToList();


            return Tuple.Create(warehousevalues, storevalues, demandvalues, psvalues);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddPackage()
        {
            var values = GetValues();
            List<SelectListItem> warehousevalues = values.Item1;
            List<SelectListItem> storevalues = values.Item2;
            List<SelectListItem> demandvalues = values.Item3;
            List<SelectListItem> psvalues = values.Item4;
            ViewBag.vlw = warehousevalues;
            ViewBag.vls = storevalues;
            ViewBag.vld = demandvalues;
            ViewBag.ps = psvalues;

            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddPackage(Package p)
        {
            
            ValidationResult result = packagevalidator.Validate(p);
            if (result.IsValid)
            {
                pm.PackageAdd(p);
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
        public ActionResult EditPackage(int id)
        {
            var values = GetValues();
            List<SelectListItem> warehousevalues = values.Item1;
            List<SelectListItem> storevalues = values.Item2;
            List<SelectListItem> demandvalues = values.Item3;
            List<SelectListItem> psvalues = values.Item4;

            ViewBag.vlw = warehousevalues;
            ViewBag.vls = storevalues;
            ViewBag.vld = demandvalues;
            ViewBag.ps = psvalues;

            var packagevalues = pm.GetByID(id);
            return View(packagevalues);

        }
        [HttpPost]
        [Authorize]
        public ActionResult EditPackage(Package p)
        {

            ValidationResult result = packagevalidator.Validate(p);
            if (result.IsValid)
            {
                Package originalPackage = pm.GetByID(p.PackageId);
                if (originalPackage.PStatusID != p.PStatusID)
                {
                    PackageTracking packageTracking = new PackageTracking
                    {
                        Barcode = p.Barcode,
                        IndexDate = DateTime.Now
                    };
                    string changeDescription = GenerateChangeDescription(originalPackage.PStatusID, p.PStatusID);
                    packageTracking.Description = changeDescription;
                    ptm.PackageTrackingAdd(packageTracking);
                }


                pm.PackageUpdate(p.PackageId,p.PStatusID,p.WarehouseId, p.StoreCode);
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


        public ActionResult DeletePackage(int id)
        {
            var packagevalue = pm.GetByID(id);
            pm.PackageDelete(packagevalue);
            return RedirectToAction("Index");
        }

        private Dictionary<int, string> GetStatusDescriptionsFromDatabase()
        {
            Dictionary<int, string> statusDescriptions = new Dictionary<int, string>();

            // Fetch demand status descriptions from the database
            var packageStatuses= psm.GetList();

            foreach (var packagestatus in packageStatuses)
            {
                statusDescriptions.Add(packagestatus.PStatusId, packagestatus.PStatusName);
            }

            return statusDescriptions;
        }
        private string GenerateChangeDescription(int oldStatusId, int newStatusId)
        {
            Dictionary<int, string> statusDescriptions = GetStatusDescriptionsFromDatabase();

            string oldStatusDescription = statusDescriptions.TryGetValue(oldStatusId, out string oldDescription) ? oldDescription : "Unknown";
            string newStatusDescription = statusDescriptions.TryGetValue(newStatusId, out string newDescription) ? newDescription : "Unknown";

            return $"Package status changed from '{oldStatusDescription}' to '{newStatusDescription}'.";
        }

    }

}


