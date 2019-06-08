using System;
using System.Web.Http;

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

using teste.Api.Models;

namespace teste.Api.Providers
{
    public class customOAuthProvider : OAuthAuthorizationServerProvider
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<Object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            userWebApiObj usuario = getUsuario(context.UserName, context.Password);

            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário ou senha invalidos.");
                return;
            }

            var identity = new ClaimsIdentity("JWT");
            identity.AddClaim(new Claim("sub",  context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            var ticket = new AuthenticationTicket(identity, null);

            context.Validated(ticket);
        }

        private userWebApiObj getUsuario(String nome, String senha)
        {
            userWebApiObj result = null;
            try
            {
                userModel _model = new userModel();
                result = _model.getuserWebApi(nome, senha);
            }
            catch (Exception ex)
            {
                Log.Error("Falha ao recuperar os dados do usuário" + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha ao recuperar os dados do usuário";
                throw new HttpResponseException(response);
            }

            return result;
        }
    }
}