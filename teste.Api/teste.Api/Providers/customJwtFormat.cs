﻿using System;
using System.Configuration;
using System.IdentityModel.Tokens;

using Thinktecture.IdentityModel.Tokens;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;

namespace teste.Api.Providers
{
    public class customJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly String _issuer = String.Empty;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public customJwtFormat(String issuer)
        {
            _issuer = issuer;
        }

        public String Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            String audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            String symmetricKeyAsBase64 = ConfigurationManager.AppSettings["as:AudienceSecret"];

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);
            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(String protectedText)
        {
            throw new NotImplementedException();
        }
    }
}