using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationGenerator.Core.Documents;

namespace PresentationGenerator.Web.HtmlHelpers
{
    public static class PresentationHelpers
    {
        public static MvcHtmlString PresentationPage(this HtmlHelper helper, Page page)
        {

            var div = new MvcHtmlString(String.Format("<div id='{0}' class='{1}'{2}{3}{4}{5}{6}>{7}</div>",
                                 page.Id,
                                 page.CSS,
                                 string.IsNullOrEmpty(page.X) ? string.Empty : " data-x='" + page.X + "'",
                                 string.IsNullOrEmpty(page.Y) ? string.Empty : " data-y='" + page.Y + "'",
                                 string.IsNullOrEmpty(page.Z) ? string.Empty : " data-z='" + page.Z + "'",
                                 string.IsNullOrEmpty(page.Rotate) ? string.Empty : " data-rotate='" + page.Rotate + "'",
                                 string.IsNullOrEmpty(page.Scale) ? string.Empty : " data-scale='" + page.Scale + "'",
                                 page.Content));
            return div;
        }
    }
}