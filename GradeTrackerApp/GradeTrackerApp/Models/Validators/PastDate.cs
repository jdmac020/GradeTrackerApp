using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradeTrackerApp.Models.Validators
{
    public class PastDate : ValidationAttribute
    {
        public override bool IsValid(object dateInput)
        {
            if (dateInput is null)
                return false;

            try
            {
                var testDate = (DateTime)dateInput;

                return testDate <= DateTime.Now;
            }
            catch(FormatException)
            {
                return false;
            }
            catch(ArgumentException)
            {
                return false;
            }

            
        }
    }
}