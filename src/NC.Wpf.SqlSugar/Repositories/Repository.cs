using NC.Wpf.Domain.Entities;
using SqlSugar;
using Volo.Abp;

namespace NC.Wpf.SqlSugar.Repositories
{
    public class Repository<TEntity> : SimpleClient<TEntity>, IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        public ISugarQueryable<TEntity> SugarQueryable => Context.Queryable<TEntity>();

        public Repository(ISqlSugarClient sqlSugarClient)
        {
            base.Context = sqlSugarClient;
        }

        //public List<TEntity> CommQuery(string json)
        //{
        //    //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
        //    throw new NotImplementedException();
        //}

        public async Task SoftDeletedAsync<TEntity>(TEntity entity) where TEntity : class, ISoftDelete, IEntity, new()
        {
            await Context.Deleteable(entity).IsLogic().ExecuteCommandAsync();
        }

        public async Task SoftDeletedByIdAsync(dynamic id)
        {
            await Context.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
        }

    }
}