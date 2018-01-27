using System;

namespace GradeTrackerApp.Core.Utility
{

    public static class DateTimeHelper
    {
        public static DateTime? OverridenDateTime { get; set; }

        public static DateTime Now
        {
            get
            {
                return OverridenDateTime ?? DateTime.Now;
            }
        }

        public static DateTime UtcNow
        {
            get
            {
                return OverridenDateTime == null ? DateTime.UtcNow : OverridenDateTime.Value.ToUniversalTime();
            }
        }
    }

}