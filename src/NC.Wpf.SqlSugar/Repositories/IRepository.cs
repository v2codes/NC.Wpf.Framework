using System.Linq.Expressions;
using NC.Wpf.Domain.Entities;
using SqlSugar;
using Volo.Abp;

namespace NC.Wpf.SqlSugar.Repositories
{
    public interface IRepository<TEntity> : ISugarRepository, ISimpleClient<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// ISugarQueryable
        /// </summary>
        ISugarQueryable<TEntity> SugarQueryable { get; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task SoftDeletedAsync<TEntity>(TEntity entity) where TEntity : class, ISoftDelete, IEntity, new();

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task SoftDeletedByIdAsync(dynamic id);

        ///// <summary>
        ///// 扩展方法，自带方法不能满足的时候可以添加新方法
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //List<TEntity> CommQuery(string json);
    }

}
