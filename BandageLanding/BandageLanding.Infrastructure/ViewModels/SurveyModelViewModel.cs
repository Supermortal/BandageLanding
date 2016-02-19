using System.Collections.Generic;
using BandageLanding.Infrastructure.Models.Survey;

namespace BandageLanding.Infrastructure.ViewModels
{
    public class SurveyModelViewModel
    {
        public long SurveyID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SurveyParameter> SurveyParameters { get; private set; }

        public SurveyModelViewModel(SurveyModel survey)
        {
            SurveyID = survey.SurveyID;
            Name = survey.Name;
            SurveyParameters = survey.SurveyParameters;
        }
    }
}
