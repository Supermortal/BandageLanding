using System.Data.Entity;
using BandageLanding.Infrastructure.Models;
using BandageLanding.Infrastructure.Models.Survey;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BandageLanding.Infrastructure
{
    public class EFContext : IdentityDbContext<ApplicationUser>
    {

        public EFContext()
            : base("name=EFContext")
        {
        }

        public DbSet<SurveyModel> SurveyModels { get; set; }
        public DbSet<SurveyInstance> SurveyInstances { get; set; }
        public DbSet<SurveyKeyValue> SurveyKeyValues { get; set; }
        public DbSet<SurveyParameter> SurveyParameters { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public static EFContext Create()
        {
            return new EFContext();
        }

    } 

}