using SqlSugar;

namespace NC.Wpf.SqlSugar
{
    public class SqlSugarOptions
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DbType? DbType { get; set; }

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 种子数据
        /// </summary>
        public bool DataSeed { get; set; } = true;

        /// <summary>
        /// Codefirst
        /// </summary>
        public bool CodeFirst { get; set; } = true;

        /// <summary>
        /// 开启sql日志
        /// </summary>
        public bool SqlLog { get; set; } = true;
    }
}
