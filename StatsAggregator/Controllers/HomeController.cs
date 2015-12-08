using StatsAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StatsAggregator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var agg = new StatsAgg();
            //agg.Test();
            //agg.AddTestAggData();
            //agg.GetDataByYear();
            //agg.GetDataByDay();
            agg.MembersAddedJourney(new FilterModel());
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}