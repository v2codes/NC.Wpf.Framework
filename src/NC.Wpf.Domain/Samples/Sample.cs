using Volo.Abp;
using SqlSugar;
using SqlSugar.DbConvert;
using NC.Wpf.Domain.Shared;
using NC.Wpf.Domain.Entities;

namespace NC.Wpf.Domain
{
    /// <summary>
    /// 示例表
    /// </summary>
    //[Table("TSamples")]
    [SugarTable("TSamples")]
    public class Sample : AuditedAggregateRoot, ISoftDelete
    {
        /// <summary>
        /// 名称
        /// </summary>
        //[Column(TypeName = "nvarchar(20)")]
        [SugarColumn(ColumnDataType = "nvarchar(50)")]  // 一般用于单个库数据库，如果多库不建议用
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        //[Column(TypeName = "nvarchar(20)")]
        [SugarColumn(ColumnDataType = "nvarchar(20)", SqlParameterDbType = typeof(EnumToStringConvert))]
        public EnumSampleType? Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sequence { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
