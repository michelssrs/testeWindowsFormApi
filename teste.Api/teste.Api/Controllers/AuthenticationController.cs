using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

using gmj.Core.Web.Util;
using gmj.Core.Web.ORM;
using gmj.Core.Monitoring;
using gmj.Core.Configuration;

using teste.Api.Models;

namespace teste.Api.Controllers
{
    public class AuthenticationController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        [Authorize]
        [Route("Authentication/userValidate")]
        public userAppObj userValidate(userAppObj _data)
        {
            userAppObj _auth = null;
            try
            {
                userModel _model = new userModel();
                _auth = _model.getuser(_data.login, _data.senha);

                return _auth;
            }
            catch (HttpResponseException httpEx)
            {
                throw httpEx;
            }
            catch (Exception ex)
            {
                Log.Error("Falha na validação do usuário " + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha na validação do usuário";

                throw new HttpResponseException(response);
            }
        }
    }
}