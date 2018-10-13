using FluentNHibernate.Mapping;
using Supero_Database.Entidade;

namespace Supero_Database.Mapeamento
{
    public class TarefaMap : ClassMap<Tarefa>
    {
        public TarefaMap()
        {
            Table("Task");
            LazyLoad();
            //Id(x => x.Id).GeneratedBy.Increment().Column("ID");
            Id(x => x.Id).GeneratedBy.Identity().Column("ID");
            //Id(x => x.Id).GeneratedBy.Assigned().Column("ID");



            Map(x => x.Titulo).Column("Titulo").Not.Nullable().Length(255);
            Map(x => x.Descricao).Column("Descricao").Length(255);
            Map(x => x.Status).Column("Cd_Status").Precision(10);
            Map(x => x.DataCriacao).Column("DataCriacao").Not.Nullable();
            Map(x => x.DataAlteracao).Column("DataAlteracao").Not.Nullable();
        }
    }
}
