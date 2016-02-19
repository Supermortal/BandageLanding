using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandageLanding.Infrastructure.Models.Survey
{
    public class SurveyParameter
    {

        public SurveyParameter()
        {
            SurveyParamTypes = new string[] { "Text", "Number", "Decimal Number", "Currency", "Date", "Multiple Choice", "Multiselect" };
            ParamRequiredChoices = new string[] { "True", "False" };
            Type = "Text";
        }

        [Key]
        public string SurveyParameterID { get; set; }

        public string Type { get; set; }
        public string Key { get; set; }
        public bool Required { get; set; }
        public virtual ICollection<SurveyParameter> Children { get; set; }
        public int Sequence { get; set; }

        public string[] SurveyParamTypes { get; set; }
        public string[] ParamRequiredChoices { get; set; }
    }
}