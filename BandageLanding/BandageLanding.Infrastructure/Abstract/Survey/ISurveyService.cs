using System.Collections.Generic;
using System.Linq;
using BandageLanding.Infrastructure.Models.Survey;

namespace BandageLanding.Infrastructure.Abstract.Survey
{
    public interface ISurveyService
    {
        IQueryable<SurveyModel> GetAllSurveys();
        void CreateSurvey(string surveyName, SurveyParameter[] surveyParameters);
        void DeleteSurvey(long surveyModelId);
        SurveyModel FindSurvey(long surveyModelId);
        void EditSurvey(long surveyModelId, string surveyName, List<SurveyParameter> surveyParameters);
        IQueryable<SurveyModel> GetActiveSurveys();
        SurveyInstance CreateSurveyInstance(SurveyKeyValue[] keyValues, long id, string userId);
        SurveyReviewViewModel CreateSurveyReviewViewModel(long surveyModelId, Dictionary<int, string> userIdToNames);
        SurveyInstanceReviewViewModel CreateSurveyInstanceReviewViewModel(long surveyInstanceId, string userName, string surveyName, long surveyModelId);
        void ActivateSurvey(bool active, long surveyModelId);
        void DeleteSurveyInstance(long surveyInstanceId);
        IEnumerable<string> GetDistinctUserIdsFromInstances(long surveyModelId);
        void SortSurveyParameters(ref SurveyModel survey);
    }
}
