using System.Web.Mvc;
using log4net;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof(AdminController));

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Mockups()
        {
            return View();
        }

        public ActionResult CMS()
        {
            return View();
        }

        public ActionResult TestBed()
        {
            return View();
        }

    }
}