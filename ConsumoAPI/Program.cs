using Entities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace ConsumoAPI
{
    class Program
    {
        #region Propriedades

        public static string Token { get; set; }

        public static List<Produto> ListaDeProdutos { get; set; }

        #endregion

        #region Métodos

        static void Main(string[] args)
        {
            Console.WriteLine("Teste API Rodando");

            Thread.Sleep(10000);

            Console.WriteLine("Teste 1");

            GetProduto();

            foreach (var item in ListaDeProdutos)
            {
                Console.WriteLine(" Codigo : " + item.Id);
                Console.WriteLine(" Nome : " + item.Nome);
            }

            Console.WriteLine("Teste 2");

            var lista =  ListarProdutos();

            foreach (var item in lista)
            {
                Console.WriteLine(" Codigo : " + item.Id);
                Console.WriteLine(" Nome : " + item.Nome);
            }

            Console.ReadLine();
        }

        #region Teste 1

        public static void GetToken()
        {
            string urlApiGeraToken = "https://localhost:5001/api/CreateToken";

            using (var cliente = new HttpClient())
            {
                string login = "teste2@teste.com.br";
                string senha = "Teste@123";
                var dados = new { Email = login, Password = senha};
                string JsonObjeto = JsonConvert.SerializeObject(dados);
                var content = new StringContent(JsonObjeto, Encoding.UTF8, "application/json");
                var resultado = cliente.PostAsync(urlApiGeraToken, content).Result;

                if (resultado.IsSuccessStatusCode)
                {
                    var tokenJson = resultado.Content.ReadAsStringAsync();

                    tokenJson.Wait();

                    Token = JsonConvert.DeserializeObject(tokenJson.Result).ToString();
                }
            }
        }

        public static void GetProduto()
        {
            GetToken(); // Gerar Token

            if (!string.IsNullOrWhiteSpace(Token))
            {
                using (var cliente = new HttpClient())
                {
                    string urlApiGeraToken = "https://localhost:5001/api/ListaProdutos";

                    cliente.DefaultRequestHeaders.Clear();

                    cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                    var response = cliente.GetStringAsync(urlApiGeraToken);

                    response.Wait();

                    var listaRetorno = JsonConvert.DeserializeObject<Produto[]>(response.Result).ToList();

                    ListaDeProdutos = listaRetorno;
                }
            }
        }

        #endregion

        #region Teste 2

        private static string ChamadaApis(HttpMethod tipoHttpMethod, string api, object objeto, bool usarLogin = false)
        {
            string retorno = String.Empty;
            string login = "teste2@teste.com.br";
            string senha = "Teste@123";

            using (HttpClient client = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(tipoHttpMethod, api))
                {
                    if (usarLogin)
                    {
                        var token = GerarToken(login, senha);

                        if (token != null)
                        {
                            client.DefaultRequestHeaders.Clear();

                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                        }
                    }

                    client.Timeout = new TimeSpan(0, 0, 100);

                    if (objeto != null)
                    {
                        string dadosSerializado = JsonConvert.SerializeObject(objeto);

                        request.Content = new StringContent(dadosSerializado, Encoding.UTF8, "application/json");
                    }

                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode) retorno = response.Content.ReadAsStringAsync().Result;
                }
            }

            return retorno;
        }

        private static string GerarToken (string login, string senha)
        {
            string urlApiGeraToken = "https://localhost:5001/api/CreateToken";
            var dados = new { Email = login, Password = senha };
            string retornoStringAPI = ChamadaApis(HttpMethod.Post, urlApiGeraToken, dados, false);
            string tokenRetorno = JsonConvert.DeserializeObject<string>(retornoStringAPI);

            return tokenRetorno;
        }

        private static List<Produto> ListarProdutos()
        {
            string urlApiGeraToken = "https://localhost:5001/api/ListaProdutos";
            string retornoStringAPI = ChamadaApis(HttpMethod.Get, urlApiGeraToken, null, true);
            List<Produto> objRetorno = JsonConvert.DeserializeObject<List<Produto>>(retornoStringAPI);

            return objRetorno;
        }

        #endregion

        #endregion
    }
}