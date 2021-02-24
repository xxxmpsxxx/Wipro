using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wipro.Service.Model;

namespace Wipro.Service.Business
{
    public class DadosBusiness
    {
        private DataAccess.DadosDataAccess DadosDataAccess = new DataAccess.DadosDataAccess();

        private const string CSV_COTACAO = "Cotacao.csv";
        private const string CSV_MOEDA = "Moeda.csv";

        public void CriarArquivoCotacao(Dados dados)
        {
            var pathMoeda = System.IO.Path.Combine(Environment.CurrentDirectory, CSV_MOEDA);
            var lstMoedas = this.DadosDataAccess.RetornarDadosMoedas(pathMoeda);

            var lstMoedasFiltered = lstMoedas.Where(x => x.id_moeda == int.Parse(dados.moeda) && 
                                                         x.data_ref >= dados.data_inicio && 
                                                         x.data_ref <= dados.data_fim);

            var pathCotacao = System.IO.Path.Combine(Environment.CurrentDirectory, CSV_COTACAO);
            var lstCotacao = this.DadosDataAccess.RetornarDadosCotacao(pathCotacao);

            var lstDadosArquivo = new List<DadosArquivo>();
            var strFile = $"moeda;data;valor\r\n";

            foreach (var item in lstMoedasFiltered)
            {
                CotacaoMoeda moeda;

                if (!Enum.IsDefined(typeof(CotacaoMoeda), item.id_moeda))
                    continue;

                moeda = (CotacaoMoeda)Enum.Parse(typeof(CotacaoMoeda), item.id_moeda.ToString());

                var lstCotacaoFiltered = lstCotacao.Where(x => x.cod_cotacao == (int)moeda &&
                                                               x.dat_cotacao >= dados.data_inicio &&
                                                               x.dat_cotacao <= dados.data_fim);

                foreach (var item1 in lstCotacaoFiltered)
                {
                    strFile += $"{item1.enum_moeda.ToString()};{string.Format("{0:yyyy-MM-dd}", item1.dat_cotacao)}{item1.vlr_cotacao}\r\n";
                }
            }

            System.IO.File.WriteAllText(System.IO.Path.Combine(Environment.CurrentDirectory, $"Resultado_{string.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now)}.csv"), strFile);
        }        
    }
}
