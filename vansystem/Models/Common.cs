using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vansystem.Models
{
    public static class Common
    {
        public static string TrimToString(this string source, int trimSize, bool withToolTip = false)
        {
            string targetStr = string.Empty;
            if (!string.IsNullOrEmpty(source))
            {
                source = targetStr = source.Replace("\n", " ");
                source = targetStr = source.Replace("</br>", " ");
                if (source.Length >= trimSize)
                {
                    targetStr = source.Substring(0, trimSize) + "<span title='" + targetStr + "' style='cursor:pointer'><b>&nbsp;...</b></span>";
                }
            }
            return targetStr;
        }
    }
}