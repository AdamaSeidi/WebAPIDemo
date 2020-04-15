using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WebAPIDemo.Controllers
{
    public class FilmesController : ApiController
    {

       
        DataClasses1DataContext dc = new DataClasses1DataContext();
       /// <summary>
       /// Dados de todos os Filmes
       /// </summary>
       /// <returns>lista do filme</returns>
        // GET: api/Filmes
        public List<Filme> Get()
        {
            var lista = from Filme in dc.Filmes orderby Filme.Titulo select Filme;
            return lista.ToList();
        }
        /// <summary>
        /// Dados do Filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns>lista</returns>
        // GET: api/Filmes/5
        public IHttpActionResult Get(int id)
        {
            var filme = dc.Filmes.SingleOrDefault(f => f.Id == id);

            if (filme != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, filme));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                "Filme não existe"));
        }
        /// <summary>
        /// Adicionar um novo Filme
        /// </summary>
        /// <param name="novoFilme"></param>
        /// <returns>novo filme</returns>

        // POST: api/Filmes
        public IHttpActionResult Post([FromBody]Filme novoFilme)
        {
            var filme = dc.Filmes.FirstOrDefault(f => f.Id == novoFilme.Id);

            if (filme != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict,
                    "Já existe um filme registado com esse ID"));     
            }
            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == novoFilme.Categoria);
            if(categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                    "Não existe ainda essa categoria, vá a categoria primeiro"));
            }

            dc.Filmes.InsertOnSubmit(novoFilme);
            try
            {
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex));
            }
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
        }
      
        /// <summary>
        /// Atualizar o Filme
        /// </summary>
        /// <param name="UpdateFilme"></param>
        /// <returns>filme atualizada</returns>
        // PUT: api/Filmes/5
        public IHttpActionResult Put([FromBody]Filme UpdateFilme)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == UpdateFilme.Id);

            if(filme == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, 
                    "Não existe nenhum filme com esse Id para poder alterar"));
            }

            Categoria categoria = dc.Categorias.FirstOrDefault(c => c.Sigla == UpdateFilme.Categoria);

            if(categoria == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound,
                   "Não existe ainda essa categoria, vá a categoria primeiro"));
            }

            filme.Titulo = UpdateFilme.Titulo;
            filme.Categoria = UpdateFilme.Categoria;

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
        /// Eleminar filme
        /// </summary>
        /// <param name="id"></param>
        /// <returns>eleminado</returns>
        // DELETE: api/Filmes/5
        public IHttpActionResult Delete(int id)
        {
            Filme filme = dc.Filmes.FirstOrDefault(f => f.Id == id);
            if (filme != null)
            {
                dc.Filmes.DeleteOnSubmit(filme);

                try
                {
                    dc.SubmitChanges();

                }catch(Exception ex)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.ServiceUnavailable, ex));
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK));
            }

            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, "" +
                "Não existe nenhum filme comesse Id para poder Eleminar"));
        }
    }
}
