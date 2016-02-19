using System.Collections.Generic;
using System.Linq;
using BandageLanding.Infrastructure.Models.Survey;

namespace BandageLanding.Infrastructure.Abstract.Survey
{
    public interface ISurveyModelRepository
    {
        IQueryable<SurveyModel> GetAll();
        SurveyModel Find(long id);
        void Delete(SurveyModel survey);
        void Delete(long id);
        long Save(SurveyModel survey);
        void Update(SurveyModel survey);
        IQueryable<SurveyModel> GetAllActive();
        IEnumerable<string> GetDistinctUserIdsFromInstances(long surveyModelId);
        void SetContext(object context);
    }
}
