
using SqlSugar;

namespace NC.Wpf.Domain.Entities
{
    [Serializable]
    public abstract class Entity : IEntity
    {

    }

    [Serializable]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
    {
        [SugarColumn(IsPrimaryKey = true)]
        public virtual TKey Id { get; set; } = default!;
    }
}
