using System.Collections.Generic;
using BandageLanding.Infrastructure.Models;

namespace BandageLanding.Infrastructure.Abstract
{
    public interface ILeadService
    {
        void CreateLead(Lead l);
        bool CheckIPExists(string ip);
        bool FrontPageHasBeenHit();
        void ContactFormFilledOut(bool contactFormFilledOut);
        bool ContactFormFilledOut();
        bool ShowContactForm();
        void ResetContactForm();
        List<Lead> GetAllLeads();
        List<Lead> GetPagedLeads(int page, int pageSize);
        LeadReturnModel GetAllLeadsReturnModel();
        LeadReturnModel GetPagedLeadsReturnModel(int page, int pageSize);
        int? GetTotalLeadCount();
    }
}
