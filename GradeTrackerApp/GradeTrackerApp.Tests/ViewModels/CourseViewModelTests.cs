using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeTrackerApp.Models.Course;
using GradeTrackerApp.Models.Evaluation;
using Xunit;
using Shouldly;

namespace GradeTrackerApp.Tests.ViewModels
{
    public class CourseViewModelTests
    {
        public CourseViewModel EvaluationViewModelFactory()
        {
            var model = new CourseViewModel {LastModified = DateTime.Parse("3/14/2018")};
            var evalList = new List<EvaluationViewModel>();
            evalList.Add(new EvaluationViewModel {LastModified = DateTime.Parse("3/15/2018")});
            evalList.Add(new EvaluationViewModel { LastModified = DateTime.Parse("3/16/2018") });
            evalList.Add(new EvaluationViewModel { LastModified = DateTime.Parse("3/17/2018") });

            var listModel = new EvaluationListViewModel(evalList);

            model.Evaluations = listModel;

            return model;
        }

        [Fact]
        public void SetLastModified_FourSeparateDates_SetsMarch17()
        {
            var testClass = EvaluationViewModelFactory();

            testClass.SetLastModified();

            testClass.LastModified.ShouldBe(DateTime.Parse("3/17/2018"));
            
        }
    }
}
