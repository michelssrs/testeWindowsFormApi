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
    public partial class principal : Form
    {
        private tokenUserObj __appToken;
        private tokenObj __appTokenResult;
        private userAppObj __appUser;

        public principal(tokenUserObj _appToken, tokenObj _appTokenResult, userAppObj _appUser)
        {
            InitializeComponent();

            __appUser = _appUser;
            __appToken = _appToken;
            __appTokenResult = _appTokenResult;

            HttpResponseMessage result = apiAccess._executeWebApiPost(_appToken.url, _appTokenResult.token_key, "Product/getAllProduct", null);
            if (result.IsSuccessStatusCode)
            {
                List<productGridObj> _productList = result.Content.ReadAsAsync<List<productGridObj>>().Result;
                dgvProduct.DataSource = _productList;
                dgvProduct.Update();
                dgvProduct.Refresh();
            }
        }

        public void updateGrid()
        {
            HttpResponseMessage result = apiAccess._executeWebApiPost(__appToken.url, __appTokenResult.token_key, "Product/getAllProduct", null);
            if (result.IsSuccessStatusCode)
            {
                List<productGridObj> _productList = result.Content.ReadAsAsync<List<productGridObj>>().Result;
                dgvProduct.DataSource = _productList;
                dgvProduct.Update();
                dgvProduct.Refresh();
            }
        }

        private void principal_Load(Object sender, EventArgs e)
        {
        }

        private void dgvProduct_CellContentClick(Object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                produtoDetalhe _frmProdutoDetalhe = new produtoDetalhe(__appToken, __appTokenResult, __appUser, Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
                _frmProdutoDetalhe.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            produtoDetalhe _frmProdutoDetalhe = new produtoDetalhe(__appToken, __appTokenResult, __appUser, 0);
            _frmProdutoDetalhe.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comanda _frmComanda = new comanda(__appToken, __appTokenResult, __appUser);
            _frmComanda.Show();
        }
    }
}