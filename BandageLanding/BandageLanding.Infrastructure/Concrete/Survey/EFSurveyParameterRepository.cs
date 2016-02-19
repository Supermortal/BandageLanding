using System;
using System.Data.Entity;
using System.Linq;
using BandageLanding.Infrastructure.Abstract.Survey;
using BandageLanding.Infrastructure.Models.Survey;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete.Survey
{
    public class EFSurveyParameterRepository : ISurveyParameterRepository 
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFSurveyParameterRepository));

        private EFContext db = new EFContext();

        public IQueryable<SurveyParameter> GetAll()
        {
            try
            {
                return db.SurveyParameters;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyParameter Find(string id)
        {
            try
            {
                return db.SurveyParameters.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Delete(SurveyParameter surveyParameter)
        {
            try
            {
                db.Entry(surveyParameter).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(string id)
        {
            try
            {
                db.Entry(db.SurveyParameters.Find(id)).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public string Save(SurveyParameter surveyParameter)
        {
            try
            {
                db.SurveyParameters.Add(surveyParameter);
                db.SaveChanges();

                return surveyParameter.SurveyParameterID;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Update(SurveyParameter surveyParameter)
        {
            try
            {
                db.Entry(surveyParameter).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void SetContext(object context)
        {
            db = (EFContext) context;
        }

    }
}