using Supero_Comum.Enum;
using Supero_Database.Abstract;
using Supero_Database.Entidade;
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

        private bool Exists(int? id)
        {
            return base.GetByWhere(t => t.Id == id).Any();
        }

        public override void Delete(Tarefa entidade)
        {
            try
            {
                base.Delete(GetById(entidade.Id));
            }
            catch (Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }

        public void Salvar(Tarefa tarefa)
        {
            try
            {
                tarefa.DataCriacao = (Exists(tarefa.Id) ? tarefa.DataCriacao : DateTime.Now);
                tarefa.DataAlteracao = DateTime.Now;
                tarefa.DataConclusao = (tarefa.DataConclusao.Year < 1000 ? DateTime.Now.AddDays(7) : tarefa.DataConclusao);

                tarefa.DescricaoStatus = GetDescricaoStatus(tarefa.Status);

                base.Save(tarefa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteById(int Id)
        {
            try
            {
                if (Exists(Id))
                {
                    base.Delete(GetById(Id));
                }
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
