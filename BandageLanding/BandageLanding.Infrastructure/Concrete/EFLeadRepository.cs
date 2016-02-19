using System;
using System.Data.Entity;
using System.Linq;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Models;
using log4net;
using Supermortal.Common.PCL.Abstract;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete
{
    public class EFLeadRepository : ILeadRepository
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof (EFLeadRepository));

        private EFContext db = new EFContext();

        public IQueryable<Lead> GetAll()
        {
            try
            {
                return db.Leads;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public Lead Find(int id)
        {
            try
            {
                return db.Leads.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Insert(Lead obj)
        {
            try
            {
                db.Leads.Add(obj);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Update(int id, Lead obj)
        {
            try
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Delete(Lead obj)
        {
            try
            {
                db.Entry(obj).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                Delete(db.Leads.Find(id));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        void IRepository<Lead, int>.Dispose()
        {
            try
            {
                db.Dispose();
                db = null;
                db = new EFContext();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public void SetContext(object context)
        {
            try
            {
                db = (EFContext) context;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public object GetContext()
        {
            try
            {
                return db;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public bool CheckIPExists(string ip)
        {
            try
            {
                return (db.Leads.FirstOrDefault(i => i.IPAddress == ip) == null);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public IQueryable<Lead> GetPagedLeads(int page, int pageSize)
        {
            try
            {
                return db.Leads.OrderBy(i => i.DateCreated).Skip((page - 1)*pageSize).Take(pageSize);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        public int GetTotalLeadCount()
        {
            try
            {
                return db.Leads.Count();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        void IDisposable.Dispose()
        {
            try
            {
                db.Dispose();
                db = null;
                db = new EFContext();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

    }
}
