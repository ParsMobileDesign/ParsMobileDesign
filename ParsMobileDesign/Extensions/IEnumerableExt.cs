using Microsoft.AspNetCore.Mvc.Rendering;
using ShirazTronic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParsMobileDesign.Extensions
{
    public static class IEnumerableExt
    {
        public static IEnumerable<SelectListItem> GetSelectListItems<T>(this IEnumerable<T> items, int selectedValue)
        {
            IEnumerable<SelectListItem> s = from item in items
                                            select new SelectListItem
                                            {
                                                Text = item.GetPropertyValue("Title"),
                                                Value = item.GetPropertyValue("PortfolioId"),
                                                Selected = item.GetPropertyValue("PortfolioId").Equals(selectedValue.ToString())
                                            };
            return s;
        }
        //public static bool HasSpecification<T>(this ICollection<T> items, string[] Values)
        //{
        //    if (items.GetType() is ICollection<ProductSpecification>)
        //    {
        //        foreach (var item in items)
        //        {
        //            int temp = (int)item.GetType().GetProperty("SpecificationValueId").GetValue(item, null);
        //            if (Values.Contains(temp.ToString()))
        //                return true;
        //        }
        //    }
        //    return false;
        //}

    }
}
