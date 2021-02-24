using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wipro.Service.Model
{
    public class Cotacao
    {
        public decimal vlr_cotacao { get; set; }
        public int cod_cotacao { get; set; }
        public DateTime dat_cotacao { get; set; }
        public CotacaoMoeda enum_moeda
        {
            get
            {
                if (cod_cotacao == 0)
                    return CotacaoMoeda.Empty;
                else
                    return (CotacaoMoeda)Enum.Parse(typeof(CotacaoMoeda), cod_cotacao.ToString());
            }
        }
    }
}
