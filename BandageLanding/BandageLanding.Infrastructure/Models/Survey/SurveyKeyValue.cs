﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BandageLanding.Infrastructure.Models.Survey
{
  public class SurveyKeyValue
  {
      [Key]
      public long SurveyKeyValueID { get; set; }
      public string Key { get; set; }
      public string Type { get; set; }
      public int? IntValue { get; set; }
      public double? DoubleValue { get; set; }
      public string StringValue { get; set; }
      //public long? LongValue { get; set; }
      public DateTime? DateTimeValue { get; set; }
      public decimal? CurrencyValue { get; set; }
      public virtual ICollection<SurveyKeyValue> Children { get; set; }
  }
}