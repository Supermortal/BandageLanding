﻿using System.Linq;

namespace BandageLanding.Infrastructure.Abstract.Survey
{
    public interface ISurveyInstanceRepository
    {
        IQueryable<Models.Survey.SurveyInstance> GetAll();
        Models.Survey.SurveyInstance Find(long id);
        void Delete(Models.Survey.SurveyInstance surveyInstance);
        void Delete(long id);
        long Save(Models.Survey.SurveyInstance surveyInstance);
        void Update(Models.Survey.SurveyInstance surveyInstance);
        void SetContext(object context);
    }
}
