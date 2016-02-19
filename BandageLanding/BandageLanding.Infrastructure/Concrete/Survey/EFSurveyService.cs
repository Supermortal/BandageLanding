using System;
using System.Collections.Generic;
using System.Linq;
using BandageLanding.Infrastructure.Abstract.Survey;
using BandageLanding.Infrastructure.Models.Survey;
using Supermortal.Common.Web.Helpers.Log;

namespace BandageLanding.Infrastructure.Concrete.Survey
{
    public class EFSurveyService : ISurveyService
    {

        private static readonly log4net.ILog Log = LogHelper.GetLogger
            (typeof(EFSurveyService));

        private readonly EFContext db = new EFContext();

        private readonly ISurveyModelRepository _smr;
        private readonly ISurveyInstanceRepository _sir;
        //private ISurveyKeyValueRepository _skvr;
        //private ISurveyParameterMemberRepository _spmr;
        //private ISurveyParameterRepository _spr;

        public EFSurveyService(ISurveyModelRepository smr, ISurveyInstanceRepository sir)//, ISurveyKeyValueRepository skvr, ISurveyParameterMemberRepository spmr, ISurveyParameterRepository spr)
        {
            _smr = smr;
            _sir = sir;
            //_skvr = skvr;
            //_spmr = spmr;
            //_spr = spr;
        }

        public IQueryable<SurveyModel> GetAllSurveys()
        {
            try
            {
                return _smr.GetAll();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void CreateSurvey(string surveyName, SurveyParameter[] surveyParameters)
        {
            try
            {
                var survey = new SurveyModel();
                survey.Name = surveyName;
                survey.DateCreated = DateTime.Now;

                var index = 0;
                foreach (var sp in surveyParameters)
                {
                    if (sp.Children != null && sp.Children.Count > 0)
                    {
                        var i = 0;
                        foreach (var s in sp.Children)
                        {
                            s.Sequence = i++;
                            s.SurveyParameterID = Guid.NewGuid().ToString();
                        }
                    }

                    sp.SurveyParameterID = Guid.NewGuid().ToString();
                    sp.Sequence = index++;
                    survey.SurveyParameters.Add(sp);
                }

                _smr.Save(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void DeleteSurvey(long id)
        {
            try
            {
                _smr.Delete(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public SurveyModel FindSurvey(long id)
        {
            try
            {
                return _smr.Find(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void EditSurvey(long surveyModelId, string surveyName, List<SurveyParameter> surveyParameters)
        {
            try
            {
                var survey = _smr.Find(surveyModelId);

                if (survey != null)
                {
                    foreach (var sp in surveyParameters)
                    {
                        db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyParameter_SurveyParameterID = {0}",
                                                  new object[] { sp.SurveyParameterID });
                    }

                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyParameters WHERE SurveyModel_SurveyID = {0}",
                                                  new object[] {surveyModelId});

                    db.SaveChanges();

                    survey.SurveyParameters.Clear();
                    var index = 0;
                    foreach (var sp in surveyParameters)
                    {
                        if (sp.Children != null && sp.Children.Count > 0)
                        {
                            var i = 0;
                            foreach (var s in sp.Children)
                            {
                                s.Sequence = i++;
                                s.SurveyParameterID = Guid.NewGuid().ToString();
                            }
                        }

                        sp.SurveyParameterID = Guid.NewGuid().ToString();
                        sp.Sequence = index++;
                        survey.SurveyParameters.Add(sp);
                    }

                    survey.Name = surveyName;
                }

                _smr.Update(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IQueryable<SurveyModel> GetActiveSurveys()
        {
            try
            {
                return _smr.GetAllActive();
            }
            catch (Exception ex) 
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyInstance CreateSurveyInstance(SurveyKeyValue[] keyValues, long id, string userId)
        {
            try
            {
                var surveyInstance = new SurveyInstance();
                surveyInstance.DateTaken = DateTime.Now;
                surveyInstance.UserId = userId;
                foreach (var keyValue in keyValues)
                {
                    surveyInstance.KeyValues.Add(keyValue);
                }

                var survey = _smr.Find(id);

                if (survey != null)
                {
                    survey.SurveyInstances.Add(surveyInstance);
                    _smr.Update(survey);
                }

                return surveyInstance;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyReviewViewModel CreateSurveyReviewViewModel(long id, Dictionary<int, string> userIdToNames)
        {
            try
            {
                var survey = _smr.Find(id);

                var surveyVM = new SurveyReviewViewModel();
                surveyVM.Survey = survey;
                surveyVM.UserNames = userIdToNames;

                return surveyVM;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public SurveyInstanceReviewViewModel CreateSurveyInstanceReviewViewModel(long surveyInstanceId, string userName, string surveyName,
                                                                              long surveyModelId)
        {
            try
            {
                var viewModel = new SurveyInstanceReviewViewModel();

                viewModel.UserName = userName;
                viewModel.SurveyName = surveyName;
                viewModel.SurveyInstance = _sir.Find(surveyInstanceId);
                viewModel.SurveyModelID = surveyModelId;

                return viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void ActivateSurvey(bool active, long id)
        {
            try
            {
                var survey = _smr.Find(id);
                survey.Active = active;

                _smr.Update(survey);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public void DeleteSurveyInstance(long id)
        {
            try
            {
                var surveyInstance = _sir.Find(id);

                if (surveyInstance != null)
                {
                    db.Database.ExecuteSqlCommand("DELETE FROM SurveyKeyValues WHERE SurveyInstance_SurveyInstanceID = {0}", new object[] { id });

                    _sir.Update(surveyInstance);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

        public IEnumerable<string> GetDistinctUserIdsFromInstances(long id)
        {
            try
            {
                return _smr.GetDistinctUserIdsFromInstances(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                return null;
            }
        }

        public void SortSurveyParameters(ref SurveyModel survey)
        {
            try
            {
                if (survey == null || survey.SurveyParameters == null)
                    return;

                foreach (var sp in survey.SurveyParameters)
                {
                    sp.Children = sp.Children.OrderBy(i => i.Sequence).ToList();
                }

                survey.SurveyParameters = survey.SurveyParameters.OrderBy(i => i.Sequence).ToList();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
        }

    }
}