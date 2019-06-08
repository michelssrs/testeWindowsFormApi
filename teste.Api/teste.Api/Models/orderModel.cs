using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using gmj.Core.Data;
using gmj.Core.Web.Util;

namespace teste.Api.Models
{
    public class orderObj
    {
        public Int32 userId { get; set; }
        public Int32 orderId { get; set; }        
        public Int32 productId { get; set; }
    }

    public class orderListObj
    {
        public String productName { get; set; }
        public DateTime productDate { get; set; }
        public DateTime orderDate { get; set; }
    }

    public class orderModel
    {
        #region App
        public orderObj insert(orderObj _data)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    INSERT 
                                        INTO systemOrder
                                            (
                                                USER_ID
                                               ,PRODUCT_ID
                                               ,ORDER_DATE
                                            )
                                        VALUES
                                            (
                                                @userId
                                               ,@productId
                                               ,GETDATE()
                                            )
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("userId",   _data.userId);
            dbQuery.CreateParameter("productId",_data.productId);

            dbQuery.ExecuteCommand();

            return _data;
        }

        public orderObj update(orderObj _data)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    UPDATE	systemOrder
                                    SET     USER_ID    = @userId
                                           ,PRODUCT_ID = @productId
                                    WHERE   ORDER_ID   = @orderId
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("orderId",  _data.orderId);
            dbQuery.CreateParameter("userId",   _data.userId);
            dbQuery.CreateParameter("productId",_data.productId);

            dbQuery.ExecuteCommand();

            return _data;
        }

        public List<orderListObj> selectAll(orderObj _data)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    SELECT	B.PRODUCT_NAME
                                           ,B.PRODUCT_DATE
	                                       ,A.ORDER_DATE
                                    FROM	systemOrder		A WITH(NOLOCK)
                                    JOIN	systemProduct	B WITH(NOLOCK) ON B.PRODUCT_ID = A.PRODUCT_ID
                                    WHERE	A.USER_ID = @userId
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("userId", _data.userId);

            IDataReader iReader = dbQuery.ExecuteDataReader(CommandBehavior.CloseConnection);

            List<orderListObj> _dataList = new List<orderListObj>();
            while (iReader.Read())
            {
                _dataList.Add
                    (
                        new orderListObj()
                        {
                            productName = Convert.ToString(iReader["PRODUCT_NAME"])
                           ,productDate = Convert.ToDateTime(iReader["PRODUCT_DATE"])
                           ,orderDate   = Convert.ToDateTime(iReader["ORDER_DATE"])
                        }
                    );
            }
            iReader.Close();

            return _dataList;
        }
        #endregion App
    }
}