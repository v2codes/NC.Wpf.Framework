
namespace NC.Wpf.Domain.Entities
{
    /// <summary>
    /// 审计实体基类
    /// </summary>
    public abstract class AuditedAggregateRoot : Entity<long>, IAuditedAggregateRoot<long>
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public virtual long? CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreationTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual long? LastModifierId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

    }
}
