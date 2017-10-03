using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCourseraWebApp.Helpers;
using MyCourseraWebApp.Models;

namespace MyCourseraWebApp.Filters
{
    public class SpecialUserTypeOnlyAttribute : AuthorizeAttribute
    {
        private int[] typesIds = null;
        private string[] typesNames = null;
        private ApplicationUserTypes[] types = null;
        private Func<ApplicationUserType, bool> predicate = null;

        public SpecialUserTypeOnlyAttribute(params int[] typesIds) : base()
        {
            this.typesIds = typesIds;
        }

        public SpecialUserTypeOnlyAttribute(params string[] typesNames) : base()
        {
            this.typesNames = typesNames;
        }

        public SpecialUserTypeOnlyAttribute(params ApplicationUserTypes[] types) : base()
        {
            this.types = types;
        }

        public SpecialUserTypeOnlyAttribute(Func<ApplicationUserType, bool> predicate) : base()
        {
            this.predicate = predicate;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool result = true;

            if (base.AuthorizeCore(httpContext))
            {
                if (typesIds != null && typesIds.Length > 0)
                {
                    if (!typesIds.Contains(ContextHelpers.CurrentLoggedInUser.UserTypeId))
                    {
                        result = false;
                    }
                }
                else if (typesNames != null && typesNames.Length > 0)
                {
                    if (!typesNames.Contains(ContextHelpers.CurrentLoggedInUser.UserType.Name))
                    {
                        result = false;
                    }
                }
                else if (types != null && types.Length > 0)
                {
                    if (!types.Any( t => t.ToString() == ContextHelpers.CurrentLoggedInUser.UserType.Name))
                    {
                        result = false;
                    }
                }
                else if (predicate != null)
                {
                    result = predicate(ContextHelpers.CurrentLoggedInUser.UserType);
                }
            }

            return result;
        }
    }
}