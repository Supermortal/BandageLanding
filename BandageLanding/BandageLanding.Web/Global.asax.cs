using System;
using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Abstract.Survey;
using BandageLanding.Infrastructure.Concrete;
using BandageLanding.Infrastructure.Concrete.Survey;
using Supermortal.Common.PCL.Concrete;
using Supermortal.Common.PCL.Helpers;
using Supermortal.Common.Web;
using Supermortal.Common.Web.Helpers;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var appDomain = AppDomain.CurrentDomain.BaseDirectory;
            var configPath = Path.Combine(appDomain, "log4net.Release.config");
#if DEBUG
            configPath = Path.Combine(appDomain, "log4net.Debug.config");
#endif

            Bootstraper.Start(new NinjectIoCHelper(), configPath);
            AddBindings();

            DependencyResolver.SetResolver(IoCWebHelper.DependencyResolver);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var log = LogHelper.GetLogger(typeof(MvcApplication));
            var ex = Server.GetLastError();
            if (!ex.Message.Contains("browserLink") && !ex.Message.Contains("/undefined"))
                log.Error(ex.Message, ex);
        }

        protected static void AddBindings()
        {
            IoCHelper.Instance.BindService<ISurveyInstanceRepository, EFSurveyInstanceRepository>();
            IoCHelper.Instance.BindService<ISurveyKeyValueRepository, EFSurveyKeyValueRepository>();
            IoCHelper.Instance.BindService<ISurveyModelRepository, EFSurveyModelRepository>();
            IoCHelper.Instance.BindService<ISurveyParameterRepository, EFSurveyParameterRepository>();
            IoCHelper.Instance.BindService<ISurveyService, EFSurveyService>();
            IoCHelper.Instance.BindService<ILeadRepository, EFLeadRepository>();
            IoCHelper.Instance.BindService<ILeadService, DefaultLeadService>();
            IoCHelper.Instance.BindService<IUserRepository, AspNetIdentityUserRepository>();
            IoCHelper.Instance.BindService<ISessionRepository, EFSessionRepository>();
            IoCHelper.Instance.BindService<ISessionService, EFSessionService>();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            SessionHelper.Instance.SessionIsStarted();
        }

    }
}
