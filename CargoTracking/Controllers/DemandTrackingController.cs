using BussinesLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace CargoTracking.Controllers
{
    public class DemandTrackingController : Controller
    {
        DemandTrackingManager dtm = new DemandTrackingManager(new EfDemandTrackingDal());

        // GET: DemandTracking
        [Authorize]
        public ActionResult Index()
        {
            var dtmvalues = dtm.GetList();
            return View(dtmvalues);
        }
    }
}