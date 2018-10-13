using Supero_Database.Entidade;
using Supero_Negocio.Controle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TesteSupero.Controllers
{
    public class TaskController : ApiController
    {
        [HttpPost]
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
    }
}
