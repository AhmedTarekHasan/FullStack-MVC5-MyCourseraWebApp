using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonUtilities;
using Microsoft.AspNet.Identity;
using MyCourseraWebApp.DTOs;
using MyCourseraWebApp.Filters;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Models;
using MyCourseraWebApp.Notifications.NotificationsAntennas;
using MyCourseraWebApp.Repositories;
using MyCourseraWebApp.ViewModel;

namespace MyCourseraWebApp.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        #region Fields
        private IUnitOfWork unitOfWork = null;
        #endregion

        #region Constructors
        public CoursesController()
        {
            unitOfWork = new UnitOfWork(new Models.ApplicationDbContext(), ContextHelpers.GlobalSystemAntenna, ContextHelpers.GlobalCoursesAntenna);
        }
        public CoursesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #endregion

        [HttpGet]
        [Route("courses/index")]
        public ActionResult Index(string searchTerm = null, int pageSize = -1, int pageIndex = 0)
        {
            ViewUserCoursesViewModel viewModel = GetCoursesForUserViewModel(false, false, searchTerm, pageSize, pageIndex);

            if (Request.IsAjaxRequest())
            {
                return PartialView("UserCourses", viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("courses/attending")]
        public ActionResult Attending(string searchTerm = null, int pageSize = -1, int pageIndex = 0)
        {
            ViewUserCoursesViewModel viewModel = GetCoursesForUserViewModel(true, false, searchTerm, pageSize, pageIndex);

            if (Request.IsAjaxRequest())
            {
                return PartialView("UserCourses", viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("courses/instructing")]
        [SpecialUserTypeOnly(ApplicationUserTypes.Instructor)]
        public ActionResult Instructing(string searchTerm = null, int pageSize = -1, int pageIndex = 0)
        {
            ViewUserCoursesViewModel viewModel = GetCoursesForUserViewModel(false, true, searchTerm, pageSize, pageIndex);

            if (Request.IsAjaxRequest())
            {
                return PartialView("UserCourses", viewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        [Route("courses/create")]
        [SpecialUserTypeOnly(ApplicationUserTypes.Instructor)]
        public ActionResult Create()
        {
            CourseDetailsViewModel viewModel = new CourseDetailsViewModel(string.Empty, string.Empty, null, null);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("courses/create")]
        [SpecialUserTypeOnly(ApplicationUserTypes.Instructor)]
        public ActionResult Create(CourseDetailsViewModel viewModel)
        {
            ActionResult result = null;

            try
            {
                if (ModelState.IsValid && ValidateCourseForCreate(viewModel, ModelState) && !CheckCourseTitleAlreadyExists(viewModel.Title))
                {
                    Course newCourse = new Models.Course(viewModel.Title, viewModel.Description, viewModel.From.Value, viewModel.To.Value);
                    unitOfWork.CoursesRepository.CreateCourse(newCourse, User.Identity.GetUserId());
                    unitOfWork.Complete();

                    Course course = unitOfWork.CoursesRepository.GetCourseById(newCourse.Id);
                    unitOfWork.CoursesAntenna.OnCourseCreated(course);

                    result = RedirectToAction("Index");
                }
                else
                {
                    result = View(viewModel);
                }
            }
            catch
            {
                result = View(viewModel);
            }

            return result;
        }

        [HttpGet]
        [Route("courses/edit/{id}")]
        [SpecialUserTypeOnly(ApplicationUserTypes.Instructor)]
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Course courseToEdit = unitOfWork.CoursesRepository.GetCourseById(id);

                if (courseToEdit != null)
                {
                    if (courseToEdit.CoursesInstructors.Any(ci => ci.InstructorId == User.Identity.GetUserId()))
                    {

                        CourseDetailsViewModel viewModel = new CourseDetailsViewModel()
                        {
                            Id = courseToEdit.Id,
                            Title = courseToEdit.Title,
                            Description = courseToEdit.Description,
                            From = courseToEdit.From.Date,
                            To = courseToEdit.To.Date,
                            IsCancelled = courseToEdit.IsCancelled
                        };

                        return View(viewModel);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("courses/edit")]
        [SpecialUserTypeOnly(ApplicationUserTypes.Instructor)]
        public ActionResult Edit(CourseDetailsViewModel viewModel)
        {
            bool done = false;

            if (ModelState.IsValid && viewModel.Id.HasValue)
            {
                Course courseToEdit = unitOfWork.CoursesRepository.GetCourseById(viewModel.Id.Value);

                if (courseToEdit != null && ValidateCourseForEdit(courseToEdit, viewModel, ModelState) && courseToEdit.CoursesInstructors.Any(ci => ci.InstructorId == User.Identity.GetUserId()))
                {
                    string oldDescription = courseToEdit.Description;
                    DateTime oldFrom = courseToEdit.From;
                    DateTime oldTo = courseToEdit.To;

                    bool isUpdated = courseToEdit.Update(viewModel.Description, viewModel.From.Value, viewModel.To.Value);

                    if (isUpdated)
                    {
                        unitOfWork.Complete();
                        done = true;

                        List<CourseUpdateToken> tokens = new List<CourseUpdateToken>();

                        if (viewModel.Description != oldDescription)
                        {
                            tokens.Add(new CourseUpdateToken() { PropertyName = "Description", OldValue = oldDescription, NewValue = viewModel.Description });
                        }

                        if (viewModel.From != oldFrom)
                        {
                            tokens.Add(new CourseUpdateToken() { PropertyName = "From", OldValue = oldFrom.ToString(), NewValue = viewModel.From.ToString() });
                        }

                        if (viewModel.To != oldTo)
                        {
                            tokens.Add(new CourseUpdateToken() { PropertyName = "To", OldValue = oldTo.ToString(), NewValue = viewModel.To.ToString() });
                        }

                        if (tokens.Count > 0)
                        {
                            unitOfWork.CoursesAntenna.OnCourseUpdated(courseToEdit, tokens);
                        }
                    }
                }
            }

            if (done)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        [Route("courses/view/{id}")]
        public ActionResult View(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                Course courseToView = unitOfWork.CoursesRepository.GetCourseById(id);

                if (courseToView != null)
                {
                    CourseDetailsViewModel viewModel = new CourseDetailsViewModel()
                    {
                        Id = courseToView.Id,
                        Title = courseToView.Title,
                        Description = courseToView.Description,
                        From = courseToView.From.Date,
                        To = courseToView.To.Date,
                        IsCancelled = courseToView.IsCancelled
                    };

                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        #region Helper Methods
        private ViewUserCoursesViewModel GetCoursesForUserViewModel(bool showMyAttendingOnly, bool showMyInstructingOnly, string searchTerm, int pageSize, int pageIndex)
        {
            ViewUserCoursesViewModel viewModel = new ViewUserCoursesViewModel();

            string userId = User.Identity.GetUserId();

            if (!string.IsNullOrEmpty(userId))
            {
                if (pageSize <= 0)
                {
                    pageSize = ContextHelpers.DefaultPageSize;
                }

                if (pageIndex < 0)
                {
                    pageIndex = 0;
                }

                PagedCollection<UserCourseDto> pagedItems = unitOfWork.CoursesRepository.GetCoursesForUserView(userId, showMyAttendingOnly, showMyInstructingOnly, searchTerm, pageSize, pageIndex);
                viewModel.Courses.AddRange(pagedItems.Items);
                viewModel.PagerToken = pagedItems.PagerToken;
            }

            return viewModel;
        }
        private bool ValidateCourseForEdit(Course courseToEdit, CourseDetailsViewModel viewModel, ModelStateDictionary modelState)
        {
            bool result = true;

            if (viewModel != null)
            {
                if (viewModel.From.HasValue)
                {
                    if (courseToEdit.CheckHasEnded())
                    {
                        modelState.AddModelError("Title", "Course has already ended");
                        result = false;
                    }
                    else
                    {
                        if (!courseToEdit.CheckHasStarted() && viewModel.From.Value.Date < DateTime.Today)
                        {
                            modelState.AddModelError("From", "From date must be greater than or equal to today");
                            result = false;
                        }

                        if (!courseToEdit.CheckHasStarted() && viewModel.To.Value.Date < DateTime.Today)
                        {
                            modelState.AddModelError("To", "To date must be greater than or equal to today");
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
        private bool ValidateCourseForCreate(CourseDetailsViewModel viewModel, ModelStateDictionary modelState)
        {
            bool result = true;

            if (viewModel != null)
            {
                if (viewModel.From.HasValue)
                {
                    if (viewModel.From.Value.Date < DateTime.Today)
                    {
                        modelState.AddModelError("From", "From date must be greater than or equal to today");
                        result = false;
                    }

                    if (viewModel.To.Value.Date < DateTime.Today)
                    {
                        modelState.AddModelError("To", "To date must be greater than or equal to today");
                        result = false;
                    }
                }
            }

            return result;
        }
        private bool CheckCourseTitleAlreadyExists(string title)
        {
            bool result = false;

            if (unitOfWork.CoursesRepository.CheckCourseTitleAlreadyExists(title))
            {
                ModelState.AddModelError("Title", "A course with the same Title already exists");
                result = true;
            }

            return result;
        }
        #endregion
    }
}
