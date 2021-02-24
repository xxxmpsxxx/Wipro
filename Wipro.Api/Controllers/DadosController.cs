using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wipro.Api.Model;

namespace Wipro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosController : ControllerBase
    {
        private static List<Dados> lstDados = new List<Dados>();

        [HttpPost("additemfile")]
        public ActionResult AddItemFila([FromBody] List<Dados> dados)
        {
            if (dados == null)
                throw new ApplicationException("Dados não informados");

            try
            {
                foreach (var item in dados)
                {
                    lstDados.Add(item);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        [HttpGet("getitemfile")]
        public Dados GetItemFila()
        {
            if (lstDados.Count == 0)
                throw new ApplicationException("Lista vazia");

            var result = lstDados.LastOrDefault();
            lstDados.Remove(result);

            return result;
        }
    }
}
