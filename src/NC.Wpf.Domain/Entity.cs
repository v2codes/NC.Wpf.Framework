using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace NC.Wpf.Domain
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class Entity : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        /// <summary>
        /// Key
        /// </summary>
        public new Guid Id { get => base.Id; set => base.Id = value; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
