using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyCourseraWebApp.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class DateIsLessThanOrEqualAttribute : ValidationAttribute, IClientValidatable
    {
        private string otherPropertyName;

        public DateIsLessThanOrEqualAttribute(string otherPropertyName, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropertyName = otherPropertyName;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            string errorMessage = ErrorMessageString;

            ModelClientValidationRule dateIsGreaterThanRule = new ModelClientValidationRule();
            dateIsGreaterThanRule.ErrorMessage = errorMessage;

            //This is the name the jQuery adapter will use
            dateIsGreaterThanRule.ValidationType = "dateislessthanorequal";

            //"otherpropertyname" is the name of the jQuery parameter for the adapter, must be LOWERCASE!
            dateIsGreaterThanRule.ValidationParameters.Add("otherpropertyname", otherPropertyName);

            yield return dateIsGreaterThanRule;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult validationResult = ValidationResult.Success;

            try
            {
                if (value != null && !string.IsNullOrEmpty(value.ToString()))
                {
                    DateTime temp;

                    if (DateTime.TryParse(value.ToString(), out temp))
                    {
                        var otherPropertyInfo = validationContext.ObjectType.GetProperty(this.otherPropertyName);

                        DateTime toValidate = (DateTime)value;
                        object referenceProperty = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

                        if (otherPropertyInfo.PropertyType.Equals(typeof(DateTime)))
                        {
                            DateTime referencePropertyDateTime = (DateTime)referenceProperty;

                            if (toValidate.CompareTo(referenceProperty) < 1)
                            {
                                validationResult = new ValidationResult(ErrorMessageString);
                            }
                        }
                        else if (otherPropertyInfo.PropertyType.Equals(typeof(DateTime?)))
                        {
                            DateTime? referencePropertyDateTime = (DateTime?)referenceProperty;

                            if (referencePropertyDateTime.HasValue && toValidate.CompareTo(referenceProperty) == 1)
                            {
                                validationResult = new ValidationResult(ErrorMessageString);
                            }
                        }
                        else
                        {
                            validationResult = new ValidationResult("An error occurred while validating the property. OtherProperty is not of type DateTime");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return validationResult;
        }
    }
}