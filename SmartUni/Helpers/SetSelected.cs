using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartUni.Helpers
{
    public static class SetSelected
    {
        public static SelectList SetSelectedValue(SelectList list, string id)
        {
            if (id != "0" && id != null)
            {
                var selected = list.Where(e => e.Value == id).FirstOrDefault();
                selected.Selected = true;
                return list;
            }
            return list;
        }
    }
}
