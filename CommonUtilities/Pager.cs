using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public class PagerToken
    {
        #region Constructors
        public PagerToken()
        {
        }
        #endregion

        #region Properties
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int ActualPageNumber { get; set; }
        public int PageFirstItemNumber { get; set; }
        public int PageLastItemNumber { get; set; }
        #endregion
    }

    public static class Pager
    {
        public static PagerToken Page(int pageSize, int pageIndex, int totalItemsCount = int.MaxValue)
        {
            PagerToken token = new PagerToken();

            token.PageSize = pageSize;
            token.TotalNumberOfPages = int.Parse(Math.Ceiling(decimal.Parse(totalItemsCount.ToString()) / decimal.Parse(pageSize.ToString())).ToString());

            if (pageIndex > token.TotalNumberOfPages - 1)
            {
                pageIndex = token.TotalNumberOfPages - 1;
            }

            token.ActualPageNumber = pageIndex + 1;
            token.PageFirstItemNumber = Math.Max(Math.Min((pageIndex * pageSize) + 1, totalItemsCount), 1);
            token.PageLastItemNumber = Math.Max(Math.Min(((pageIndex * pageSize) + pageSize - 1) + 1, totalItemsCount), 1);

            return token;
        }
    }
}
