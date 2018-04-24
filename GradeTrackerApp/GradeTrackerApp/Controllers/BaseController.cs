using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradeTrackerApp.Domain.Shared;
using GradeTrackerApp.Models;

namespace GradeTrackerApp.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult GradeTrackerError(IDomainModel errorModel, IViewModel viewModel)
        {
            var errorViewModel = new GradeTrackerErrorViewModel((ErrorDomainModel)errorModel);

            errorViewModel.ViewModel = viewModel;

            return View("GradeTrackerError", errorViewModel);
        }
    }
}