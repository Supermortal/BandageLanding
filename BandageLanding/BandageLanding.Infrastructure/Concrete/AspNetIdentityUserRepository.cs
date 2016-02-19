using System;
using System.Linq;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Models;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Supermortal.Common.PCL.Abstract;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete
{
    public class AspNetIdentityUserRepository : IUserRepository
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof (AspNetIdentityUserRepository));

        private readonly UserManager<ApplicationUser> _um;

        public AspNetIdentityUserRepository()
        {
            var userStore = new UserStore<ApplicationUser>(new EFContext());
            _um = new UserManager<ApplicationUser>(userStore);
        }

        public IQueryable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser Find(string id)
        {
            try
            {
                return _um.FindByIdAsync(id).Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Insert(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public void Update(string id, ApplicationUser obj)
        {
            _um.UpdateAsync(obj);
        }

        public void Delete(ApplicationUser obj)
        {
            try
            {
                _um.DeleteAsync(obj);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                Delete(Find(id));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        void IRepository<ApplicationUser, string>.Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetContext(object context)
        {
            throw new NotImplementedException();
        }

        public object GetContext()
        {
            throw new NotImplementedException();
        }

        public ApplicationUser FindByUserName(string userName)
        {
            try
            {
                return _um.FindByNameAsync(userName).Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
