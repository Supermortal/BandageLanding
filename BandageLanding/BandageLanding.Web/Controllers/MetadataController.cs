using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using BandageLanding.Filters;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Models;
using log4net;
using Microsoft.AspNet.Identity.Owin;
using Supermortal.Common.PCL.Helpers;
using Supermortal.Common.PCL.Models;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Controllers
{
    [TokenFilter]
    public class MetadataController : ApiController
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof (MetadataController));

        private readonly ILeadService _ls = (ILeadService)IoCHelper.Instance.GetService<ILeadService>();
        private readonly ISessionService _ss = (ISessionService) IoCHelper.Instance.GetService<ISessionService>();

        [HttpGet]
        public ServerReturnModel<List<Lead>> GetLeads(int? page, int? pageSize)
        {
            try
            {
                if (page == null || pageSize == null)
                    return new ServerReturnModel<List<Lead>>("Success", _ls.GetAllLeads());

                return new ServerReturnModel<List<Lead>>("Success", _ls.GetPagedLeads((int)page, (int)pageSize));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new ServerReturnModel<List<Lead>>("Error", ex.Message);
            }
        }

        [HttpGet]
        public ServerReturnModel<int?> GetTotalLeadsCount()
        {
            try
            {
                return new ServerReturnModel<int?>("Success", _ls.GetTotalLeadCount());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return new ServerReturnModel<int?>("Error", ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public ServerReturnModel<string> LogIn([FromBody]PostUser user)
        {
            try
            {
                var password = Crypto.DecryptAes(Convert.FromBase64String(user.Password), "CH45%kjf*&",
                    new byte[] {123, 156, 22, 86, 2, 67, 86, 45, 92, 108});

                var sim = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
                var result = sim.PasswordSignIn(user.UserName, password, false, false);

                var srm = new ServerReturnModel<string>();
                if (result == SignInStatus.Success)
                {
                    var session = _ss.CreateSession(user.UserName);

                    srm.Status = "Success";
                    srm.Value = session.Token;

                    return srm;
                }

                srm.Status = "Error";
                srm.Message = "Invalid signin information";

                return srm;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);

                var srm = new ServerReturnModel<string>
                {
                    Status = "Error",
                    Message = "Exception on signin"
                };

                return srm;
            }
        }

    }

    public class PostUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
