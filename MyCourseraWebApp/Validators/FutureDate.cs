using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCourseraWebApp.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FutureDateAttribute : ValidationAttribute, IClientValidatable
    {
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = ErrorMessageString;

            ModelClientValidationRule futureDateRule = new ModelClientValidationRule();
            futureDateRule.ErrorMessage = errorMessage;

            //This is the name the jQuery adapter will use
            futureDateRule.ValidationType = "futuredate";

            yield return futureDateRule;
        }

        public override bool IsValid(object value)
        {
            bool result = true;

            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                DateTime temp;
                result = (DateTime.TryParse(value.ToString(), out temp) && temp.Date >= DateTime.Today);
            }

            return result;
        }
    }
}