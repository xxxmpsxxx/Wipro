using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Wipro.Service.Client
{
    public class BaseClient
    {
        public const string URL_BASE = @"https://enderecoAPi";

        /// <summary>
        /// Garante que o código de retorno é menor que 400.   
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <exception cref="ClientResponseException"></exception>
        protected void EnsureSuccessStatusCode(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode >= 400)
            {
                string message = httpResponseMessage.Content.ReadAsStringAsync().Result;

                throw new ClientResponseException(
                    httpResponseMessage.StatusCode,
                    message,
                    httpResponseMessage.Content);
            }
        }

        /// <summary>
        /// Garante que o código de retorno é menor que 400.   
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <exception cref="ClientResponseException"></exception>
        protected async Task EnsureSuccessStatusCodeAsync(HttpResponseMessage httpResponseMessage)
        {
            if ((int)httpResponseMessage.StatusCode >= 400)
            {
                string message = await httpResponseMessage.Content.ReadAsStringAsync();

                throw new ClientResponseException(
                    httpResponseMessage.StatusCode,
                    message,
                    httpResponseMessage.Content);
            }
        }

        private HttpClient CreateHttpClient(
           ref string url,
            Dictionary<string, object> querys = null,
            Dictionary<string, object> headers = null)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("URL não pode ser vazia ou nula.");
            }

            if (url.Contains('?'))
            {
                throw new ArgumentException("URL já contem parametros. (?)");
            }

            //HACK: para ignorar certificados inválidos, expirados do servidor
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            HttpClient cliente = new HttpClient(httpClientHandler);
            if (querys != null)
            {
                url += "?";

                foreach (var query in querys)
                {
                    url += query.Key + "=" + System.Net.WebUtility.UrlEncode((query.Value ?? "").ToString()) + "&";
                }
            }
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    cliente.DefaultRequestHeaders.Add(header.Key, header.Value.ToString());
                }
            }

            return cliente;
        }
        protected async Task<HttpResponseMessage> GetAsync(
            string url,
            Dictionary<string, object> querys = null,
            Dictionary<string, object> headers = null)
        {
            using (HttpClient cliente = CreateHttpClient(ref url, querys, headers))
            {
                return await cliente.GetAsync(url);
            }
        }

        protected async Task<HttpResponseMessage> PostAsync(
            string url,
            object body = null,
            Dictionary<string, object> querys = null,
            Dictionary<string, object> headers = null)
        {
            using (HttpClient cliente = CreateHttpClient(ref url, querys, headers))
            {
                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);

                var stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                return await cliente.PostAsync(url, stringContent);
            }
        }

        protected async Task<HttpResponseMessage> PutAsync(
            string url,
            object body,
            Dictionary<string, object> querys = null,
            Dictionary<string, object> headers = null)
        {
            using (HttpClient cliente = CreateHttpClient(ref url, querys, headers))
            {
                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body);

                var stringContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                return await cliente.PutAsync(url, stringContent);
            }
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string url,
            Dictionary<string, object> querys = null,
            Dictionary<string, object> headers = null)
        {
            using (HttpClient cliente = CreateHttpClient(ref url, querys, headers))
            {
                return await cliente.DeleteAsync(url);
            }
        }
    }
}
