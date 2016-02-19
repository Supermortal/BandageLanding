using System.Linq;

namespace BandageLanding.Infrastructure.Abstract.Survey
{
    public interface ISurveyParameterRepository
    {
        IQueryable<Models.Survey.SurveyParameter> GetAll();
        Models.Survey.SurveyParameter Find(string id);
        void Delete(Models.Survey.SurveyParameter surveyParameter);
        void Delete(string id);
        string Save(Models.Survey.SurveyParameter surveyParameter);
        void Update(Models.Survey.SurveyParameter surveyParameter);
        void SetContext(object context);
    }
}
