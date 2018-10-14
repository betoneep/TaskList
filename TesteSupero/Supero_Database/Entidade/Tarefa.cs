using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supero_Database.Entidade
{
    public class Tarefa
    {
        public virtual int Id { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual int? Status { get; set; }
        public virtual string DescricaoStatus { get; set; }
        public virtual DateTime DataCriacao { get; set; }
        public virtual DateTime DataAlteracao { get; set; }
        public virtual DateTime DataConclusao { get; set; }
    }
}
