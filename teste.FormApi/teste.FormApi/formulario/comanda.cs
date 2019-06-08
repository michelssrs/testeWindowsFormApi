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
    public partial class comanda : Form
    {
        private tokenUserObj __appToken;
        private tokenObj __appTokenResult;
        private userAppObj __appUser;

        public comanda(tokenUserObj _appToken, tokenObj _appTokenResult, userAppObj _appUser)
        {
            InitializeComponent();

            __appUser = _appUser;
            __appToken = _appToken;
            __appTokenResult = _appTokenResult;

            orderObj _data = new orderObj();
            _data.userId = _appUser.id;

            HttpResponseMessage result = apiAccess._executeWebApiPost(_appToken.url, _appTokenResult.token_key, "Order/getAllOrder", _data);
            if (result.IsSuccessStatusCode)
            {
                List<orderListObj> _comandaList = result.Content.ReadAsAsync<List<orderListObj>>().Result;
                dgvComanda.DataSource = _comandaList;
                dgvComanda.Update();
                dgvComanda.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}