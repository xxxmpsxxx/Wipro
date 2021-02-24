using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wipro.Service.Model;

namespace Wipro.Service.Client
{
    public class WiproClient : BaseClient
    {
        private const string URL_GET_ITEM_FILA = @"/api/Dados/getitemfila";

        public async Task<Dados> GetItemFila()
        {
            var response = await GetAsync($"{URL_BASE}{URL_GET_ITEM_FILA}");

            await EnsureSuccessStatusCodeAsync(response);

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Dados>(json);
        }
    }
}
