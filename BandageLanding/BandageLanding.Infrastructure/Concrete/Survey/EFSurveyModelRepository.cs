using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BandageLanding.Infrastructure.Abstract.Survey;
using BandageLanding.Infrastructure.Models.Survey;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete.Survey
{
    public class EFSurveyModelRepository : ISurveyModelRepository
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
                (typeof(EFSurveyModelRepository));

        private EFContext db = new EFContext();

        public IQueryable<SurveyModel> GetAll()
        {
            try
            {
                return db.SurveyModels;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyModel Find(long id)
        {
            try
            {
                return db.SurveyModels.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void Delete(SurveyModel survey)
        {
            try
            {
                if (survey != null)
                {
                    foreach (var sp in survey.SurveyParameters)
                    {
                        db.Database.ExecuteSqlCommand(
                            "DELETE FROM SurveyParameters WHERE SurveyParameter_SurveyParameterID = {0}",
                            new object[] { sp.SurveyParameterID });
                    }

                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyModel_SurveyID = {0}",
                        new object[] { survey.SurveyID });

                    foreach (var surveyInstance in survey.SurveyInstances)
                    {
                        db.Database.ExecuteSqlCommand(
                            "DELETE FROM SurveyKeyValues WHERE SurveyInstance_SurveyInstanceID = {0}",
                            new object[] { surveyInstance.SurveyInstanceID });
                    }

                    db.Dispose();
                    db = null;
                    db = new EFContext();
                    survey = db.SurveyModels.Find(survey.SurveyID);

                    survey.SurveyInstances.Clear();

                    db.SurveyModels.Remove(survey);
                    db.SaveChanges();

                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyInstances WHERE SurveyModel_SurveyID IS NULL");
                }
            }
            catch
                (Exception
                    ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void Delete(long id)
        {
            try
            {
                var survey = db.SurveyModels.Find(id);
                if (survey == null) return;

                foreach (var sp in survey.SurveyParameters)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyParameter_SurveyParameterID = {0}",
                        new object[] { sp.SurveyParameterID });
                }

                db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyModel_SurveyID = {0}",
                    new object[] { id });

                foreach (var surveyInstance in survey.SurveyInstances)
                {
                    foreach (var kv in surveyInstance.KeyValues)
                    {
                        db.Database.ExecuteSqlCommand("DELETE FROM SurveyKeyValues WHERE SurveyKeyValue_SurveyKeyValueID = {0}",
                            new object[] { kv.SurveyKeyValueID });
                    }

                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyKeyValues WHERE SurveyInstance_SurveyInstanceID = {0}",
                        new object[] { surveyInstance.SurveyInstanceID });
                }

                db.Dispose();
                db = null;
                db = new EFContext();
                survey = db.SurveyModels.Find(id);

                survey.SurveyInstances.Clear();

                db.SurveyModels.Remove(survey);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("DELETE FROM SurveyInstances WHERE SurveyModel_SurveyID IS NULL");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public long Save(SurveyModel survey)
        {
            try
            {
                db.SurveyModels.Add(survey);
                db.SaveChanges();

                return survey.SurveyID;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return -1;
            }
        }

        public void Update(SurveyModel survey)
        {
            try
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IQueryable<SurveyModel> GetAllActive()
        {
            try
            {
                return db.SurveyModels.Where(sm => sm.Active);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public IEnumerable<string> GetDistinctUserIdsFromInstances(long surveyModelId)
        {
            try
            {
                return db.SurveyModels.Find(surveyModelId).SurveyInstances.Select(si => si.UserId).Distinct();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void SetContext(object context)
        {
            db = (EFContext)context;
        }

    }
}