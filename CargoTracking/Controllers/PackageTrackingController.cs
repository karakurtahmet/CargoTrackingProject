using BussinesLayer.Concrete;
using CargoTracking.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoTracking.Controllers
{
    public class PackageTrackingController : Controller
    {
        // GET: PackageTracking
        PackageTrackingManager ptm = new PackageTrackingManager(new EfPackageTracking());
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ScanBarcode(BarcodeViewModel viewModel)
        {
            List<PackageTracking> packageTrackings = ptm.GetByBarcode(viewModel.BarcodeNumber);

            // Pass the barcode number to the view using ViewBag
            ViewBag.BarcodeNumber = viewModel.BarcodeNumber;

            return View(packageTrackings);
        }

        // warehouse koli sayısı
        // http request ********** yarın rıdvan abi sorar
        // user talep işlemleri ve koli takip user

    }

}