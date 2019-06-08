using System;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;

using teste.Api.Models;


namespace teste.Api.Controllers
{
    public class OrderController : ApiController
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpPost]
        [Authorize]
        [Route("Order/salve")]
        public orderObj salve(orderObj _data)
        {
            try
            {
                orderModel _model = new orderModel();

                if (_data.orderId == 0)
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
                Log.Error("Falha ao salvar a comanda " + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha ao salvar a comanda";

                throw new HttpResponseException(response);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("Order/getAllOrder")]
        public List<orderListObj> getAllOrder(orderObj _data)
        {
            List<orderListObj> _listProduct = new List<orderListObj>();
            try
            {
                orderModel _model = new orderModel();
                _listProduct = _model.selectAll(_data);

                return _listProduct;
            }
            catch (HttpResponseException httpEx)
            {
                throw httpEx;
            }
            catch (Exception ex)
            {
                Log.Error("Falha na listagem das comandas " + "\r\n" + ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace);
                HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                response.ReasonPhrase = "Falha na listagem das comandas";

                throw new HttpResponseException(response);
            }
        }
    }
}