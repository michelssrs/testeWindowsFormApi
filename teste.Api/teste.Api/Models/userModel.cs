using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using gmj.Core.Data;
using gmj.Core.Web.Util;

namespace teste.Api.Models
{
    public class userAppObj
    {
        public Int32 id { get; set; }
        public String nome { get; set; }
        public String login { get; set; }
        public String senha { get; set; }
    }

    public class userWebApiObj
    {
        public Int32 id { get; set; }
        public String nome { get; set; }
        public String senha { get; set; }
    }

    public class userModel
    {
        #region WebApi
        public userWebApiObj getuserWebApi(String nome, String senha)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    SELECT	A.USER_ID
	                                       ,A.USER_NAME
	                                       ,A.USER_PASSWORD
                                    FROM	webapiUser   A WITH(NOLOCK)
                                    WHERE	A.USER_NAME       = @systemNm
                                    AND     A.USER_PASSWORD   = @systemPws
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("systemNm",  nome);
            dbQuery.CreateParameter("systemPws", senha);

            IDataReader iReader = dbQuery.ExecuteDataReader(CommandBehavior.CloseConnection);

            userWebApiObj _data = new userWebApiObj();
            while (iReader.Read())
            {
                _data.id    = Convert.ToInt32(iReader["USER_ID"]);
                _data.nome  = Convert.ToString(iReader["USER_NAME"]);
                _data.senha = Convert.ToString(iReader["USER_PASSWORD"]);
            }
            iReader.Close();

            return _data;
        }
        #endregion WebApi

        #region App
        public userAppObj getuser(String nome, String senha)
        {
            #region QuerySQL
            String _stQuery = String.Format
                                (@"
                                    SELECT	A.USER_ID
	                                       ,A.USER_NAME
                                           ,A.USER_LOGIN
	                                       ,A.USER_PASSWORD
                                    FROM	systemUser   A WITH(NOLOCK)
                                    WHERE	A.USER_LOGIN      = @systemNm
                                    AND     A.USER_PASSWORD   = @systemPws
                                  ");
            #endregion QuerySQL

            gmjDatabase dbQuery = new gmjDatabase();
            dbQuery.CommandText = _stQuery;
            dbQuery.CommandType = CommandType.Text;
            dbQuery.CommandTimeout = 240000;

            dbQuery.CreateParameter("systemNm",  nome);
            dbQuery.CreateParameter("systemPws", senha);

            IDataReader iReader = dbQuery.ExecuteDataReader(CommandBehavior.CloseConnection);

            userAppObj _data = new userAppObj();
            while (iReader.Read())
            {
                _data.id = Convert.ToInt32(iReader["USER_ID"]);
                _data.nome = Convert.ToString(iReader["USER_NAME"]);
            }
            iReader.Close();

            return _data;
        }
        #endregion App
    }
}