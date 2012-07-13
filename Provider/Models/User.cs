using System;
using System.Text.RegularExpressions;

namespace Provider.Models
{
    public class User
    {
        internal static Uri ClaimedIdentifierBaseUri {
            get { return Util.GetAppPathRootedUri("user/"); }
        }

        internal static Uri GetClaimedIdentifierForUser(string username) {
            if (string.IsNullOrEmpty(username)) {
                throw new ArgumentNullException("username");
            }

            return new Uri(ClaimedIdentifierBaseUri, username.ToLowerInvariant());
        }

        internal static string GetUserFromClaimedIdentifier(Uri claimedIdentifier) {
            Regex regex = new Regex(@"/user/([^/\?]+)");
            Match m = regex.Match(claimedIdentifier.AbsoluteUri);
            if (!m.Success) {
                throw new ArgumentException();
            }

            return m.Groups[1].Value;
        }
    }
}