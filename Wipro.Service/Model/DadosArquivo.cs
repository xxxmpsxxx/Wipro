using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wipro.Service.Model
{
    public class DadosArquivo
    {
        public string moeda { get; set; }
        public DateTime data { get; set; }        
        public decimal valor_cotacao { get; set; }
    }
}
