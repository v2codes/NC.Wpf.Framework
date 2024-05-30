
namespace NC.Wpf.Domain.Entities
{
    public interface IAuditedAggregateRoot : IEntity
    {

    }

    public interface IAuditedAggregateRoot<TKey> : IEntity<TKey>, IAuditedAggregateRoot
    {

        DateTime? LastModificationTime { get; set; }

        long? LastModifierId { get; set; }

        DateTime? CreationTime { get; set; }

        long? CreatorId { get; set; }
    }
}
