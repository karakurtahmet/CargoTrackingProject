using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules;
using DataAccessLayer;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CargoTracking.Controllers
{
    public class AdminDemandController : Controller
    {
        DemandValidator demandvalidator = new DemandValidator();
        DemandManager dm = new DemandManager(new EfDemandDal());
        FirmManager fm = new FirmManager(new EfFirmDal());
        UserManager um = new UserManager(new EfUserDal());
        StoreManager sm = new StoreManager(new EfStoreDal());
        DemandStatusManager dsm = new DemandStatusManager(new EfDemandStatusDal());
        DemandTrackingManager dtm = new DemandTrackingManager(new EfDemandTrackingDal());
        PackageManager pm = new PackageManager(new EfPackageDal());
        // GET: AdminDemand
        [Authorize]
        public ActionResult Index()
        {
            var demandvalues = dm.GetList();
            return View(demandvalues);
        }
        [Authorize]
        [HttpGet]
        public ActionResult AddDemand()
        {


            List<SelectListItem> uservalues = (from x in um.GetList()
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.UserId.ToString(),
                                               }).ToList();
            ViewBag.vlu = uservalues;


            List<SelectListItem> demandStatusValues = (from ds in dsm.GetList()
                                                       select new SelectListItem
                                                       {
                                                           Text = ds.DemandStatusName,
                                                           Value = ds.DemandStatusId.ToString(),
                                                       }).ToList();
            ViewBag.DemandStatusList = demandStatusValues;

            List<SelectListItem> storeValues = (from x in sm.GetList()
                                                select new SelectListItem
                                                {
                                                    Text = x.StoreName,
                                                    Value = x.StoreCode,
                                                }).ToList();

            ViewBag.StoreList = storeValues;
            ViewBag.StorePackage = GetPackageCountsForStores();
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddDemand(Demand d)
        {

            //for dropdown list for user

            //for status 
            int selectedDemandStatusId;
            if (int.TryParse(Request.Form["StatusID"], out selectedDemandStatusId))
            {
                d.StatusId = selectedDemandStatusId;

            }
            else
            {
                d.StatusId = 0;
            }


            // for other attributes
            d.DemandDate = DateTime.Now;
            d.RejectionReason = "NULL";
            var storePackage = GetPackageCountsForStores();
            var PackageNumber = storePackage[d.StoreCode];

            ValidationResult result = demandvalidator.Validate(d);


            if (result.IsValid)
            {
                if (d.Quantity > PackageNumber)
                {
                    ModelState.AddModelError("", "Talep kolisi mevcut koliden fazla");
                    return RedirectToAction("AddDemand");
                }

                dm.DemandAdd(d);
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

        public ActionResult AcceptDemand(int id)
        {
            var demandvalues = dm.GetById(id);
            demandvalues.StatusId = 4;

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

            return RedirectToAction("Index");
        }
        public ActionResult RefuseDemand(int id)
        {
            var demandvalues = dm.GetById(id);
            demandvalues.StatusId = 3;

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
            return RedirectToAction("Index");

        }

        public ActionResult RefuseAcceptedDemand(int id)
        {
            var demandvalues = dm.GetById(id);
            demandvalues.StatusId = 5;
            Demand originalDemand = dm.GetById(demandvalues.DemandId);

            DemandTracking demandTracking = new DemandTracking
            {
                DemandId = demandvalues.DemandId,
                UserId = demandvalues.UserId,
                DemandStatusId = demandvalues.StatusId,
                IndexDate = DateTime.Now
            };
            string changeDescription = GenerateChangeDescription(4, demandvalues.StatusId);
            demandTracking.ChangeDescription = changeDescription;
            dtm.DemandTrackingAdd(demandTracking);

            dm.DemandUpdate(demandvalues);

            return RedirectToAction("Index");

        }
        public ActionResult ConfirmDemand(int id)
        {
            var demandvalues = dm.GetById(id);
            demandvalues.StatusId = 6;
            Demand originalDemand = dm.GetById(demandvalues.DemandId);

            DemandTracking demandTracking = new DemandTracking
            {
                DemandId = demandvalues.DemandId,
                UserId = demandvalues.UserId,
                DemandStatusId = demandvalues.StatusId,
                IndexDate = DateTime.Now
            };
            //aynı mağazaya ait kolileri çekip onlara bir demand id atıyoruz
            var packagesHasSameStore = pm.GetList().Where(x => x.StoreCode == demandvalues.StoreCode);
            var remainingQuantity = demandvalues.Quantity;
            foreach (var item in packagesHasSameStore)
            {
                item.DemandID = demandvalues.DemandId;
                pm.PackageUpdate(item);
                remainingQuantity--;
                if (remainingQuantity <= 0)
                {
                    break;
                }
            }


            string changeDescription = GenerateChangeDescription(4, demandvalues.StatusId);
            demandTracking.ChangeDescription = changeDescription;
            dtm.DemandTrackingAdd(demandTracking);

            dm.DemandUpdate(demandvalues);

            return RedirectToAction("Index");

        }
        //[HttpGet]
        //public ActionResult EditDemand(int id)
        //{

        //    List<SelectListItem> uservalues = (from x in um.GetList()
        //                                       select new SelectListItem
        //                                       {
        //                                           Text = x.Name,
        //                                           Value = x.UserId.ToString(),
        //                                       }).ToList();

        //    ViewBag.vlu = uservalues;

        //    List<SelectListItem> demandStatusValues = (from ds in dsm.GetList()
        //                                               select new SelectListItem
        //                                               {
        //                                                   Text = ds.DemandStatusName,
        //                                                   Value = ds.DemandStatusId.ToString(),
        //                                               }).ToList();
        //    ViewBag.DemandStatusList = demandStatusValues;


        //    var demandvalues = dm.GetById(id);
        //    return View(demandvalues);

        //}

        //[HttpPost]
        //public ActionResult EditDemand(Demand d)
        //{

        //    int selectedDemandStatusId;
        //    if (int.TryParse(Request.Form["StatusID"], out selectedDemandStatusId))
        //    {
        //        d.StatusId = selectedDemandStatusId;

        //    }
        //    else
        //    {
        //        d.StatusId = 0;
        //    }

        //    d.DemandDate = DateTime.Now;
        //    d.RejectionReason = null;

        //    ValidationResult result = demandvalidator.Validate(d);

        //    if (result.IsValid)
        //    {

        //        Demand originalDemand = dm.GetById(d.DemandId);
        //        if (originalDemand.StatusId != d.StatusId) 
        //        {
        //            DemandTracking demandTracking = new DemandTracking
        //            {
        //                DemandId = d.DemandId,
        //                UserId = d.UserId,
        //                DemandStatusId = d.StatusId,
        //                IndexDate = DateTime.Now
        //            };
        //            string changeDescription = GenerateChangeDescription(originalDemand.StatusId, d.StatusId);
        //            demandTracking.ChangeDescription = changeDescription;
        //            dtm.DemandTrackingAdd(demandTracking);
        //        }


        //        dm.DemandUpdate(d.DemandId, d.StatusId);
        //        return RedirectToAction("Index");

        //    }
        //    else
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
        //        }
        //    }
        //    return View();

        //}
        //public ActionResult DeleteDemand(int id) 
        //{
        //    var demandvalues = dm.GetById(id);
        //    dm.DemandDelete(demandvalues);
        //    return RedirectToAction("Index");
        //}

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

        private string GenerateChangeDescription(int oldStatusId, int newStatusId)
        {
            Dictionary<int, string> statusDescriptions = GetStatusDescriptionsFromDatabase();

            string oldStatusDescription = statusDescriptions.TryGetValue(oldStatusId, out string oldDescription) ? oldDescription : "Unknown";
            string newStatusDescription = statusDescriptions.TryGetValue(newStatusId, out string newDescription) ? newDescription : "Unknown";

            return $"Demand status changed from '{oldStatusDescription}' to '{newStatusDescription}'.";
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

    }
}