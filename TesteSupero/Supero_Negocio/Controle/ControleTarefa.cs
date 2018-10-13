using Supero_Database.Abstract;
using Supero_Database.Entidade;
using Supero_Negocio.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Negocio.Controle
{
    public class ControleTarefa : AbstractRepositorio<Tarefa>
    {
        public override Tarefa GetById(int id)
        {
            return base.GetByWhere(t => t.Id == id).FirstOrDefault();
        }

        public void Salvar(Tarefa tarefa)
        {
            try
            {
                tarefa.DataAlteracao = DateTime.Now;
                tarefa.DataAlteracao = DateTime.Now;
                tarefa.DataCriacao = DateTime.Now;
                tarefa.DescricaoStatus = GetDescricaoStatus(tarefa.Status);
                base.Save(tarefa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override IList<Tarefa> Get()
        {
            try
            {
                var tarefas = base.Get();

                tarefas.ToList().ForEach(item => item.DescricaoStatus = GetDescricaoStatus(item.Status));

                return tarefas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetDescricaoStatus(int? parId)
        {
            try
            {
                switch (parId)
                {
                    case (short)Enumeradores.Status.Ativa:
                        return "Ativa";
                    case (short)Enumeradores.Status.Cancelada:
                        return "Cancelada";
                    case (short)Enumeradores.Status.Nova:
                        return "Nova";
                    case (short)Enumeradores.Status.Excluida:
                        return "Excluida";
                    default:
                        return String.Empty;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Teste()
        {
        //    var teste = Get().FirstOrDefault();


        //    var teste2 = new Tarefa
        //    {
        //        CdStatus = 1,
        //        Descricao = "TESTE",
        //        DescricaoStatus = "ewewe",
        //        Titulo = "ewew",
        //        DataAlteracao = DateTime.Now,
        //        DataCriacao = DateTime.Now
        //};

        //    base.Save(teste2);


        //    base.Save(teste);
        }
    }
}
