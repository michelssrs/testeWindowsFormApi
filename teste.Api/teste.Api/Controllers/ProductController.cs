using System;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;

using teste.Api.Models;


namespace teste.Api.Controllers
{
    public class ProductController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        [Authorize]
        [Route("Product/salve")]
        public productObj salve(productObj _data)
        {
            try
            {
                prodcutModel _model = new prodcutModel();

                if (_data.productId == 0)
                    _data = _model.insert(_data);
                else
                    _data = _model.update(_data);

                return _data;
            }
            catch (HttpResponseException httpEx)
            {
                throw httpEx;
            }
            catch (Exception ex)
            {
                Log.Error("Falha ao salvar o produto " + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha ao salvar o produto";

                throw new HttpResponseException(response);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Product/select")]
        public productObj select(productObj _data)
        {
             try
            {
                prodcutModel _model = new prodcutModel();
                _data = _model.select(_data);

                return _data;
            }
            catch (HttpResponseException httpEx)
            {
                throw httpEx;
            }
            catch (Exception ex)
            {
                Log.Error("Falha na seleção do produto " + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha na seleção do produto";

                throw new HttpResponseException(response);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Product/getAllProduct")]
        public List<productObj> getAllProduct()
        {
            List<productObj> _listProduct = new List<productObj>();
            try
            {
                prodcutModel _model = new prodcutModel();
                _listProduct = _model.selectAll();

                return _listProduct;
            }
            catch (HttpResponseException httpEx)
            {
                throw httpEx;
            }
            catch (Exception ex)
            {
                Log.Error("Falha na listagem dos produtos " + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha na listagem dos produtos";

                throw new HttpResponseException(response);
            }
        }
    }
}