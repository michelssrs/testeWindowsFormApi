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

namespace teste.FormApi
{
    public partial class produtoDetalhe : Form
    {
        private Int32 _productId;
        private tokenUserObj __appToken;
        private tokenObj __appTokenResult;
        private userAppObj __appUser;
        
        public produtoDetalhe(tokenUserObj _appToken, tokenObj _appTokenResult, userAppObj _appUser, Int32 productId)
        {
            InitializeComponent();

            __appUser = _appUser;
            __appToken = _appToken;
            __appTokenResult = _appTokenResult;

            _productId = productId;

            if (productId >0)
            { 
                productObj _data = new productObj();
                _data.productId = productId;

                HttpResponseMessage result = apiAccess._executeWebApiPost(_appToken.url, _appTokenResult.token_key, "Product/select", _data);
                if (result.IsSuccessStatusCode)
                {
                    _data = result.Content.ReadAsAsync<productObj>().Result;

                    codigoTxt.Text      = _data.productId.ToString();
                    valorTxt.Text       = _data.productPrice.ToString();
                    nomeTxt.Text        = _data.productNome.ToString();
                    quantidadeTxt.Text  = _data.productAmount.ToString();
                    dataTxt.Text        = _data.productDate.ToString("dd/MM/yyyy hh:mm:ss");
                }
                else
                    label6.Text = "Houve um erro ao consultar o produto selecionado.";
            }

            dataTxt.Enabled = false;
            codigoTxt.Enabled = false;

            if (productId == 0)
                button3.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            principal frmPrincipal = new principal(__appToken, __appTokenResult, __appUser);
            frmPrincipal.updateGrid();

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(nomeTxt.Text) && !String.IsNullOrEmpty(quantidadeTxt.Text) && !String.IsNullOrEmpty(valorTxt.Text))
                {
                    productObj _data = new productObj();
                    _data.productId = ((String.IsNullOrEmpty(codigoTxt.Text)) ? 0 : Convert.ToInt32(codigoTxt.Text));
                    _data.productNome = nomeTxt.Text;
                    _data.productPrice = Convert.ToDecimal(valorTxt.Text);
                    _data.productAmount = Convert.ToInt32(quantidadeTxt.Text);

                    HttpResponseMessage result = apiAccess._executeWebApiPost(__appToken.url, __appTokenResult.token_key, "Product/salve", _data);
                    if (result.IsSuccessStatusCode)
                    {
                        _data = result.Content.ReadAsAsync<productObj>().Result;

                        label6.Text = "PRODUTO GRAVADO COM SUCESSO.";
                    }
                    else
                        label6.Text = "Houve um erro ao salvar os dados.";
                }
                else
                    label6.Text = "OS CAMPOS 'NOME', 'VALOR' e 'QUANTIDADE' são obrigatórios.";
            }
            catch (Exception ex)
            {
                label6.Text = String.Format("Houve um problema na execução do comando. Descrição: {0}", ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            orderObj _data = new orderObj();
            _data.productId = _productId;
            _data.userId    = __appUser.id;

            HttpResponseMessage result = apiAccess._executeWebApiPost(__appToken.url, __appTokenResult.token_key, "Order/salve", _data);
            if (result.IsSuccessStatusCode)
            {
                _data = result.Content.ReadAsAsync<orderObj>().Result;

                label6.Text = "PRODUTO INCLUÍDO NA COMANDA.";
            }
            else
                label6.Text = "Houve um erro ao adicionar o produto na comanda.";
        }
    }
}