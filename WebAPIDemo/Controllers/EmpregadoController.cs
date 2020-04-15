using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class EmpregadoController : ApiController
    {
        private List<Empregado> Funcionarios;

        public EmpregadoController()
        {
            Funcionarios = new List<Empregado>
            {
                new Empregado{ Id = 1, Nome = "Adama", Apelido = "Seidi"},
                new Empregado{ Id = 2, Nome = "Maria", Apelido = "Lopes"},
                new Empregado{ Id = 3, Nome = "Carlos", Apelido = "Fernandes"},
            };

        }

        // GET: api/Empregado
        public List<Empregado> Get()
        {
            return Funcionarios;
        }

        // GET: api/Empregado/5
        /// <summary>
        /// Dados completo do empregado
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>returna empregado</returns>
        public Empregado Get(int id)
        {
            return Funcionarios.FirstOrDefault(x => x.Id == id);
        }


        //GET: api/Empregado/GetNomes
        /// <summary>
        /// Nome proprio de todos os empregados
        /// </summary>
        /// <returns>lista com os nomes de todos os empregados</returns>
        [Route("api/Empregado/GetNomes")] //apatir do terceiro get deve-se especifica com route
        public List<string> GetNomes()
        {
            List<string> output = new List<string>();

            foreach(var nome in Funcionarios)
            {
                output.Add(nome.Nome);
            }
            return output;
        }

        // POST: api/Empregado
        /// <summary>
        /// Registro de novo empregado
        /// </summary>
        /// <param name="valor">Empregado</param>
        public void Post([FromBody]Empregado valor)
        {
            Funcionarios.Add(valor);
        }


        // PUT: api/Empregado/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Empregado/5
        public void Delete(int id)
        {
        }
    }
}
