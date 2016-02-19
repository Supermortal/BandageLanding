using System.Linq;
using BandageLanding.Infrastructure.Models;
using Supermortal.Common.PCL.Abstract;

namespace BandageLanding.Infrastructure.Abstract
{
    public interface ILeadRepository : IRepository<Lead, int>
    {
        bool CheckIPExists(string ip);
        IQueryable<Lead> GetPagedLeads(int page, int pageSize);
        int GetTotalLeadCount();
    }
}
