using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Interactors.Course;

namespace GradeTrackerApp.Tests.Interactors
{
    public static class InteractorFactory
    {
        public static CreateCourse CreateCourse()
        {
            return new CreateCourse();
        }
    }
}
