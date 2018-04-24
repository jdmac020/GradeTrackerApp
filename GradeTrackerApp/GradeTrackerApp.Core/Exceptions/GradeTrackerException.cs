using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeTrackerApp.Core.Exceptions
{
    public class GradeTrackerException : Exception
    {

        public string Name { get; set; }

        public GradeTrackerException(string name, string message) : base(message)
        {
            Name = name;
        }

        public GradeTrackerException(string name, string message, Exception innerException) : base(message, innerException)
        {
            Name = name;
        }

        protected GradeTrackerException()
        {
            
        }

        
    }
    
}
