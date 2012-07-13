using System;
using System.Web;

namespace Provider.Models
{
    internal static class Util
    {
        internal static Uri GetAppPathRootedUri(string value) {
            string appPath = HttpContext.Current.Request.ApplicationPath.ToLowerInvariant();
            if (!appPath.EndsWith("/")) {
                appPath += "/";
            }

            return new Uri(HttpContext.Current.Request.Url, appPath + value);
        }
    }
}