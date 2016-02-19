using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BandageLanding.Infrastructure.Abstract;
using BandageLanding.Infrastructure.Models;
using log4net;
using Supermortal.Common.Web.Helpers;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete
{
    public class DefaultLeadService : ILeadService
    {

        private static readonly ILog Log = LogHelper.GetLogger(typeof (DefaultLeadService));

        private readonly ILeadRepository _lr;

        public DefaultLeadService(ILeadRepository lr)
        {
            _lr = lr;
        }

        public void CreateLead(Lead l)
        {
            try
            {
                ContactFormFilledOut(true);

                l.DateCreated = DateTime.Now;
                l.IPAddress = SessionHelper.Instance.IPAddress;
                _lr.Insert(l); 
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public bool CheckIPExists(string ip)
        {
            try
            {
                return _lr.CheckIPExists(ip);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool FrontPageHasBeenHit()
        {
            try
            {
                if (HttpContext.Current.Session["FrontPageHasBeenHit"] == null)
                {
                    HttpContext.Current.Session["FrontPageHasBeenHit"] = true;
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }
        }

        public void ContactFormFilledOut(bool contactFormFilledOut)
        {
            try
            {
                HttpContext.Current.Session["ContactFormFilledOut"] = contactFormFilledOut;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public bool ContactFormFilledOut()
        {
            try
            {
                if (HttpContext.Current.Session["ContactFormFilledOut"] == null)
                    return false;

                return (bool) HttpContext.Current.Session["ContactFormFilledOut"];
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return false;
            }
        }

        public bool ShowContactForm()
        {
            try
            {
                if (ContactFormFilledOut())
                    return false;

                if (!FrontPageHasBeenHit())
                    return true;

                if (HttpContext.Current.Session["ContactFormHits"] == null)
                    HttpContext.Current.Session["ContactFormHits"] = 0;

                var contactFormHits = (int) HttpContext.Current.Session["ContactFormHits"];

                if (contactFormHits++ == 2)
                {
                    if (contactFormHits == 3)
                        contactFormHits = 0;

                    HttpContext.Current.Session["ContactFormHits"] = contactFormHits;

                    return true;
                }

                HttpContext.Current.Session["ContactFormHits"] = contactFormHits;
                return false;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return true;
            }
        }

        public void ResetContactForm()
        {
            try
            {
                HttpContext.Current.Session["ContactFormHits"] = null;
                HttpContext.Current.Session["ContactFormFilledOut"] = null;
                HttpContext.Current.Session["FrontPageHasBeenHit"] = null;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public List<Lead> GetAllLeads()
        {
            try
            {
                return _lr.GetAll().ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public List<Lead> GetPagedLeads(int page, int pageSize)
        {
            try
            {
                return _lr.GetPagedLeads(page, pageSize).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public LeadReturnModel GetAllLeadsReturnModel()
        {
            try
            {
                if (HttpContext.Current.Session["_totalLeadCount"] == null)
                    HttpContext.Current.Session["_totalLeadCount"] = _lr.GetTotalLeadCount();

                var count = (int) HttpContext.Current.Session["_totalLeadCount"];

                return new LeadReturnModel(count, GetAllLeads());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public LeadReturnModel GetPagedLeadsReturnModel(int page, int pageSize)
        {
            try
            {
                var count = _lr.GetTotalLeadCount();
                return new LeadReturnModel(count, GetPagedLeads(page, pageSize));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public int? GetTotalLeadCount()
        {
            try
            {
                return _lr.GetTotalLeadCount();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

    }
}
