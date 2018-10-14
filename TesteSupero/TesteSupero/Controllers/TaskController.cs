using Supero_Comum.Util;
using Supero_Database.Entidade;
using Supero_Negocio.Controle;
using System;
using System.Web.Http;

namespace TesteSupero.Controllers
{
    public class TaskController : ApiController
    {
        [HttpPost]
        [ActionName("SalvarTarefa")]
        public IHttpActionResult Salvar(Tarefa tarefa)
        {
            try
            {
                Controller.ControleTarefa().Salvar(tarefa);

                return Ok(tarefa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetTarefas()
        {
            try
            {
           
                return Ok(Controller.ControleTarefa().Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ActionName("RemoverTarefa")]
        public IHttpActionResult Delete([FromBody]string Id)
        {
            try
            {
                Controller.ControleTarefa().DeleteById(Id.ToInt());

                return Ok($"Tarefa [{Id}] removida com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
