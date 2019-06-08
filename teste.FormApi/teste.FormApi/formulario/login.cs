using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace teste.FormApi
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void acessar_Click(Object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(usuarioTxt.Text) && !String.IsNullOrEmpty(senhaTxt.Text))
            {
                tokenUserObj _appToken = new tokenUserObj();
                _appToken.url = ConfigurationManager.AppSettings["urlApi"];
                _appToken.user = ConfigurationManager.AppSettings["apiUser"];
                _appToken.password = ConfigurationManager.AppSettings["apiPws"];

                tokenObj _appTokenResult = apiAccess._getToken(_appToken);

                userAppObj _userLogin = new userAppObj();
                _userLogin.login = usuarioTxt.Text;
                _userLogin.senha = senhaTxt.Text;

                if (!String.IsNullOrEmpty(_appTokenResult.token_key))
                {
                    HttpResponseMessage result = apiAccess._executeWebApiPost(_appToken.url, _appTokenResult.token_key, "Authentication/userValidate", _userLogin);
                    if (result.IsSuccessStatusCode)
                    {
                        _userLogin = result.Content.ReadAsAsync<userAppObj>().Result;

                        if (_userLogin.id > 0)
                        {
                            principal _prc = new principal(_appToken, _appTokenResult, _userLogin);
                            _prc.Show();
                        }
                        else
                            label3.Text = "USUÁRIO OU SENHA INVALIDOS, POR FAVOR TENTE NOVAMENTE";
                    }
                    else
                        label3.Text = "USUÁRIO OU SENHA INVALIDOS, POR FAVOR TENTE NOVAMENTE";
                }
                else
                    label3.Text = "NÃO POSSÍVEL CONECTAR NO SERVIDOR, POR FAVOR TESTE NOVAMENTE";
            }
            label3.Text = "POR FAVOR, INFORME O LOGIN E A SENHA, ANTES DE CLICAR EM ACESSAR!";
        }
    }
}