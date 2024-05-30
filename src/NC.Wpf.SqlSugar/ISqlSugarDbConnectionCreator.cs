using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NC.Wpf.SqlSugar
{
    public interface ISqlSugarDbConnectionCreator
    {
        //SqlSugarOptions Options { get; }

        #region Aop
        //Action<ISqlSugarClient> OnSqlSugarClientConfig { get; set; }
        //Action<object, DataAfterModel> DataExecuted { get; set; }
        //Action<object, DataFilterModel> DataExecuting { get; set; }
        //Action<string, SugarParameter[]> OnLogExecuting { get; set; }
        //Action<string, SugarParameter[]> OnLogExecuted { get; set; }
        //Action<PropertyInfo, EntityColumnInfo> EntityService { get; set; }

        void AddTableFilters(ISqlSugarClient sqlSugarClient);
        void EntityService(PropertyInfo property, EntityColumnInfo column);
        void DataExecuting(object oldValue, DataFilterModel entityInfo);
        void DataExecuted(object oldValue, DataAfterModel entityInfo);
        void OnLogExecuting(string sql, SugarParameter[] pars);
        void OnLogExecuted(string sql, SugarParameter[] pars, ISqlSugarClient sqlSugarClient);
        #endregion

        ConnectionConfig Build(Action<ConnectionConfig>? action = null);

        void SetAopEvents(ISqlSugarClient sqlSugarClient);
    }
}
