using System;
using System.Web.Mvc;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Models;
using log4net;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Controllers
{
    public class HomeController : Controller
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof (HomeController));

        private readonly ILeadService _ls;

        public HomeController(ILeadService ls)
        {
            _ls = ls;
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ShowContactForm = _ls.ShowContactForm();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Lead l)
        {
            try
            {
                if (ModelState.IsValid)
                    _ls.CreateLead(l);
                else
                    _ls.ResetContactForm();

                ViewBag.ShowContactForm = _ls.ShowContactForm();

                return View();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new HttpStatusCodeResult(500);
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult LeadForm()
        {
            try
            {
                var lead = new Lead();
                return PartialView(lead);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public PartialViewResult LeadForm(Lead lead)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView(lead);

                _ls.CreateLead(lead);

                return PartialView(new Lead());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}