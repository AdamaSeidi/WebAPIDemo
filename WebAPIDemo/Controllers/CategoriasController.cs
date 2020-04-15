using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIDemo.Controllers
{
    public class CategoriasController : ApiController
    {
        DataClasses1DataContext dc = new DataClasses1DataContext();

        /// <summary>
        /// Dados completos de todas as Categorias
        /// </summary>
        /// <returns> lista de categoria</returns> 
        // GET: api/Categorias
        public List<Categoria> Get()
        {
            var lista = from Categoria in dc.Categorias select Categoria;
            return lista.ToList();
        }
        /// <summary>
        /// Dados da Categoria
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns>lista</returns>

        // GET: api/Categorias/AC
        [Route("api/categorias/{sigla}")]
        public IHttpActionResult Get(string sigla)
        {
            var categoria = dc.Categorias.SingleOrDefault(c => c.Sigla == sigla);
            if(categoria != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, categoria));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "Categoria não existe"));
        }

        /// <summary>
        /// Adicionar uma Categoria
        /// </summary>
        /// <param name="novaCategoria"></param>
        /// <returns>nova Categoria</returns>
        // POST: api/Categorias
        public IHttpActionResult Post([FromBody]Categoria novaCategoria)
        {
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == novaCategoria.Sigla);

            if (categoria != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict,
                   "Já existe uma categoria registada com sigla"));
            }
            dc.Categorias.InsertOnSubmit(novaCategoria);
            try
            {
                dc.SubmitChanges();
            }catch(Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }

        /// <summary>
        /// Atualizar a Categoria
        /// </summary>
        /// <param name="updateCategoria"></param>
        /// <returns>categoria atualizada</returns>
        // PUT: api/Categorias/5
        public IHttpActionResult Put([FromBody]Categoria updateCategoria)
        {
            Categoria categoria = dc.Categorias.
                FirstOrDefault(c => c.Sigla == updateCategoria.Sigla);
            if (categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                  "Não existe nenhuma categoria com essa com sigla para poder alterar"));

            }

            categoria.Sigla = updateCategoria.Sigla;
            categoria.Categoria1 = updateCategoria.Categoria1;

            try
            {
                dc.SubmitChanges();

            }catch(Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }
        /// <summary>
        /// Eleminar categoria
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns>eleminada</returns>
        // DELETE: api/Categorias/CM
        [Route("api/categorias/{sigla}")]
        public IHttpActionResult Delete(string sigla)
        {
            Categoria categoria = dc.Categorias.
             FirstOrDefault(c => c.Sigla == sigla);

            if(categoria != null)
            {
                dc.Categorias.DeleteOnSubmit(categoria);

                try
                {
                    dc.SubmitChanges();

                }catch(Exception ex)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex));
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                  "Não existe nenhuma categoria com essa com sigla para poder eleminar"));
        }
    }
}
