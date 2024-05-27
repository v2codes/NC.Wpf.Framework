using System.ComponentModel.DataAnnotations.Schema;
using NC.Wpf.Domain.Shared;

namespace NC.Wpf.Domain
{
    /// <summary>
    /// 示例表
    /// </summary>
    [Table("TSamples")]
    public class Sample : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [Column(TypeName = "nvarchar(20)")]
        public EnumSampleType? Type { get;set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sequence { get; set; }

    }
}
