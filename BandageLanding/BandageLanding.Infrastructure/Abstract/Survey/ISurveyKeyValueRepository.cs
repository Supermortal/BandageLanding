﻿using System.Linq;

namespace BandageLanding.Infrastructure.Abstract.Survey
{
    public interface ISurveyKeyValueRepository
    {
        IQueryable<Models.Survey.SurveyKeyValue> GetAll();
        Models.Survey.SurveyKeyValue Find(long id);
        void Delete(Models.Survey.SurveyKeyValue surveyKeyValue);
        void Delete(long id);
        long Save(Models.Survey.SurveyKeyValue surveyKeyValue);
        void Update(Models.Survey.SurveyKeyValue surveyKeyValue);
        void SetContext(object context);
    }
}
