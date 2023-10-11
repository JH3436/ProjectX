using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;
using SmartBreadcrumbs.Attributes;

namespace MVC_Project.Controllers
{
    public class ACTController : Controller
    {
        private readonly ProjectXContext _context;

        public ACTController(ProjectXContext context)
        {
            _context = context;
        }

        [Breadcrumb("所有活動", FromAction = nameof(MyActivityController.HomePage), FromController = typeof(MyActivityController))]
        public ActionResult ACT()
        {
            var categories = _context.MyActivity.Select(m => m.Category).Distinct().Take(5).ToList();
            List<ResponseActivity> activities = new List<ResponseActivity>();

            foreach (var category in categories)
            {
                var myActivityData = from m in _context.MyActivity
                                     join o in _context.OfficialPhoto
                                     on m.ActivityID equals o.ActivityID
                                     where m.Category == category
                                     select new ResponseActivity
                                     {
                                         ActivityName = m.ActivityName,
                                         Category = m.Category,
                                         SuggestedAmount = m.SuggestedAmount,
                                         ActivityContent = m.ActivityContent,
                                         MinAttendee = m.MinAttendee,
                                         VoteDate = m.VoteDate,
                                         ExpectedDepartureMonth = m.ExpectedDepartureMonth,
                                         PhotoPath = o.PhotoPath
                                     };

                activities.AddRange(myActivityData.Take(5).ToList());
            }

            var viewModel = new HomePageViewModel
            {
                Activities = activities
            };

            return View(viewModel);
        }
    }
}