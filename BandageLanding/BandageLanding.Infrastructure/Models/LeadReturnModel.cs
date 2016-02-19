using System.Collections.Generic;

namespace BandageLanding.Infrastructure.Models
{
    public class LeadReturnModel
    {
        public int TotalCount { get; set; }
        public List<Lead> Leads { get; set; }

        public LeadReturnModel()
        {
            
        }

        public LeadReturnModel(int totalCount, List<Lead> leads)
        {
            TotalCount = totalCount;
            Leads = leads;
        }
    }
}
