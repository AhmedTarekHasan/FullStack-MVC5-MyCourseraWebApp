using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyCourseraWebApp.Validators;

namespace MyCourseraWebApp.ViewModel
{
    public class CourseDetailsViewModel
    {
        #region Consturctors
        public CourseDetailsViewModel()
        {
            
        }
        public CourseDetailsViewModel(string title, string description, DateTime? from, DateTime? to)
        {
            Title = title;
            Description = description;
            From = from;
            To = to;
            IsCancelled = false;
        }
        #endregion

        #region Properties
        public int? Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        //[FutureDate(ErrorMessage = "From date must be greater than or equal to today")]
        [DateIsLessThanOrEqual("To", "From date must be less than or equal to To date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? From { get; set; }

        [Required]
        //[FutureDate(ErrorMessage = "To date must be greater than or equal to today")]
        [DateIsGreaterThanOrEqual("From", "To date must be greater than or equal to From date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? To { get; set; }

        [Display(Name = "Is Cancelled")]
        public bool IsCancelled { get; set; }
        #endregion
    }
}