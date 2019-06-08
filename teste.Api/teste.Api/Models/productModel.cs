using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using gmj.Core.Data;
using gmj.Core.Web.Util;

namespace teste.Api.Models
{
    public class productObj
    {
        public Int32 productId { get; set; }

        public Int32 productAmount { get; set; }
        public String productNome { get; set; }
        public Decimal productPrice { get; set; }
        public DateTime productDate { get; set; }
    }

    public class prodcutModel
    {
        #region App
        public productObj insert(productObj _data)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    INSERT 
                                        INTO systemProduct
                                            (
                                                PRODUCT_NAME
                                               ,PRODUCT_PRICE
	                                           ,PRODUCT_AMOUNT
                                               ,PRODUCT_DATE
                                            )
                                        VALUES
                                            (
                                                @productName
                                               ,@productPrice
                                               ,@productAmont
                                               ,GETDATE()
                                            )
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("productName",  _data.productNome);
            dbQuery.CreateParameter("productPrice", _data.productPrice);
            dbQuery.CreateParameter("productAmont", _data.productAmount);

            dbQuery.ExecuteCommand();

            return _data;
        }

        public productObj update(productObj _data)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    UPDATE	systemProduct
                                    SET     PRODUCT_NAME  = @productName
                                           ,PRODUCT_PRICE = @productPrice
                                           ,PRODUCT_AMOUNT= @productAmont
                                    WHERE   PRODUCT_ID    = @productId
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("productId",    _data.productId);
            dbQuery.CreateParameter("productName",  _data.productNome);
            dbQuery.CreateParameter("productPrice", _data.productPrice);
            dbQuery.CreateParameter("productAmont", _data.productAmount);

            dbQuery.ExecuteCommand();

            return _data;
        }

        public productObj select(productObj _data)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    SELECT	A.PRODUCT_ID
	                                       ,A.PRODUCT_NAME
                                           ,A.PRODUCT_PRICE
	                                       ,A.PRODUCT_DATE
                                           ,A.PRODUCT_AMOUNT
                                    FROM	systemProduct   A WITH(NOLOCK)
                                    WHERE   A.PRODUCT_ID = @productId
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("productId", _data.productId);

            IDataReader iReader = dbQuery.ExecuteDataReader(CommandBehavior.CloseConnection);

            while (iReader.Read())
            {
                _data.productId = Convert.ToInt32(iReader["PRODUCT_ID"]);
                _data.productNome = Convert.ToString(iReader["PRODUCT_NAME"]);
                _data.productPrice = Convert.ToDecimal(iReader["PRODUCT_PRICE"]);
                _data.productAmount = Convert.ToInt32(iReader["PRODUCT_AMOUNT"]);
                _data.productDate = Convert.ToDateTime(iReader["PRODUCT_DATE"]);
            }
            iReader.Close();

            return _data;
        }

        public List<productObj> selectAll()
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    SELECT	A.PRODUCT_ID
	                                       ,A.PRODUCT_NAME
                                           ,A.PRODUCT_PRICE
	                                       ,A.PRODUCT_DATE
                                           ,A.PRODUCT_AMOUNT
                                    FROM	systemProduct   A WITH(NOLOCK)
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            IDataReader iReader = dbQuery.ExecuteDataReader(CommandBehavior.CloseConnection);

            List<productObj> _data = new List<productObj>();
            while (iReader.Read())
            {
                _data.Add
                    (
                        new productObj()
                        {
                            productId = Convert.ToInt32(iReader["PRODUCT_ID"])
                           ,productNome = Convert.ToString(iReader["PRODUCT_NAME"])
                           ,productPrice = Convert.ToDecimal(iReader["PRODUCT_PRICE"])
                        }
                    );
            }
            iReader.Close();

            return _data;
        }
        #endregion App
    }
}