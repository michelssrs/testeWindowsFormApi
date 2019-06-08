using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace teste.FormApi
{
    class apiAccess
    {
        public static tokenObj _getToken(tokenUserObj pParam)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pParam.url);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(String.Format("grant_type=password&password={0}&username={1}", pParam.password, pParam.user));
            HttpResponseMessage response = client.PostAsync("oauth/token", stringContent).Result;

            tokenObj token =null;
            if (response.IsSuccessStatusCode)
            {
                token = response.Content.ReadAsAsync<tokenObj>().Result;               
                token.token_key = String.Format("{0} {1}", token.token_type, token.access_token);
            }

            return token;
        }

        public static HttpResponseMessage _executeWebApiPost(String pHost, String pToken, String pParam, Object pObjet)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(pHost);

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", pToken);

            HttpResponseMessage response = client.PostAsJsonAsync(pParam, pObjet).Result;

            return response;
        }
    }
}