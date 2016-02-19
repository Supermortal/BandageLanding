using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandageLanding.Infrastructure.Models.Survey
{
  public class SurveyInstance
  {

      public SurveyInstance()
      {
          KeyValues = new List<SurveyKeyValue>();
      }

      [Key]
      public long SurveyInstanceID { get; set; }
      public DateTime DateTaken { get; set; }
      public string UserId { get; set; }
      public virtual ICollection<SurveyKeyValue> KeyValues { get; private set; }    
  }
}