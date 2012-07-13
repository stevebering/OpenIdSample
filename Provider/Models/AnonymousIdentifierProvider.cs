﻿using System;
using System.Web.Security;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Provider;

namespace Provider.Models
{
    public class AnonymousIdentifierProvider
        : PrivatePersonalIdentifierProviderBase
    {
        public AnonymousIdentifierProvider()
            : base(Util.GetAppPathRootedUri("anon?id=")) { }

        /// <summary>
        /// Gets the salt to use for generating an anonymous identifier for a given OP local identifier.
        /// </summary>
        /// <param name="localIdentifier">The OP local identifier.</param>
        /// <returns>The salt to use in the hash.</returns>
        /// <remarks>
        /// It is important that this method always return the same value for a given
        /// <paramref name="localIdentifier"/>.
        /// New salts can be generated for local identifiers without previously assigned salt
        /// values by calling <see cref="CreateSalt"/> or by a custom method.
        /// </remarks>
        protected override byte[] GetHashSaltForLocalIdentifier(Identifier localIdentifier) {
            // This is just a sample with no database... a real web app MUST return 
            // a reasonable salt here and have that salt be persistent for each user.
            var membership = (ReadOnlyXmlMembershipProvider)Membership.Provider;
            string username = User.GetUserFromClaimedIdentifier(new Uri(localIdentifier));
            string salt = membership.GetSalt(username);

            if (salt != null) {
                return Convert.FromBase64String(salt);
            }

            base.NewSaltLength = 5;
            var user = membership.GetUser(username, false);
            var newSalt = base.CreateSalt();
            user.Salt = newSalt;
            return newSalt;

            // If users were encountered without a salt, one could be generated like this,
            // and would also need to be saved to the user's account.
            //// var newSalt = AnonymousIdentifierProviderBase.GetNewSalt(5);
            //// user.Salt = newSalt;
            //// return newSalt;
        }
    }
}