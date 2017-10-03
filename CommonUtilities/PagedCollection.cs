using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtilities
{
    public class PagedCollection<T>
    {
        #region Constructors
        public PagedCollection()
        {
        }
        public PagedCollection(List<T> items)
        {
            Items = items;
        }
        #endregion

        #region Properties
        public List<T> Items { get; set; }
        public PagerToken PagerToken { get; set; }
        #endregion
    }
}
