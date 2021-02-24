using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wipro.Service.Model;

namespace Wipro.Service.DataAccess
{
    public class DadosDataAccess
    {
        public List<Moedas> RetornarDadosMoedas(string path)            
        {
            var lst = new List<Moedas>();

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {                    
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        var dado = field.Split(';');
                        lst.Add(new Moedas()
                        {
                            id_moeda = int.Parse(dado[0]),
                            data_ref = DateTime.Parse(dado[1])
                        });
                    }
                }
            }

            return lst;
        }

        public List<Cotacao> RetornarDadosCotacao(string path)
        {
            var lst = new List<Cotacao>();

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        var dado = field.Split(';');
                        lst.Add(new Cotacao()
                        {
                            vlr_cotacao = decimal.Parse(dado[0]),
                            cod_cotacao = int.Parse(dado[1]),
                            dat_cotacao = DateTime.Parse(dado[2])
                        });
                    }
                }
            }

            return lst;
        }
    }
}
