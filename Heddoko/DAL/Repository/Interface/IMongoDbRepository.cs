using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using DAL.Models;

namespace DAL
{
    public interface IMongoDbRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<GetOneResult<TEntity>> GetOne(FilterDefinition<TEntity> filter);

        /// <summary>
        /// A generic GetOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GetOneResult<TEntity>> GetOne(string id);

        /// <summary>
        /// A Generic get many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<GetManyResults<TEntity>> GetMany(IEnumerable<string> ids);

        /// <summary>
        /// a generic get many method with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<GetManyResults<TEntity>> GetMany(FilterDefinition<TEntity> filter);

        /// <summary>
        /// GetMany with filter and projection
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        IFindFluent<TEntity, TEntity> FindCursor(FilterDefinition<TEntity> filter);

        /// <summary>
        /// a generic get all method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        Task<GetManyResults<TEntity>> GetAll();

        /// <summary>
        /// a generic Exists method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Exists(string id);

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<long> Count(string id);

        /// <summary>
        /// A generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<long> Count(FilterDefinition<TEntity> filter);

        /// <summary>
        /// A generic Add One method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Result> AddOne(TEntity item);

        /// <summary>
        /// a generic delete one method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Result> DeleteOne(string id);

        /// <summary>
        /// a generic delete many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<Result> DeleteMany(IEnumerable<string> ids);

        #region Update
        /// <summary>
        /// UpdateOne by id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<Result> UpdateOne(string id, UpdateDefinition<TEntity> update);

        /// <summary>
        /// UpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<Result> UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);

        /// <summary>
        /// UpdateMany with Ids
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<Result> UpdateMany(IEnumerable<string> ids, UpdateDefinition<TEntity> update);

        /// <summary>
        /// Update with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        Task<Result> UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);
        #endregion Update

        #region Find And Update

        /// <summary>
        /// GetAndUpdateOne with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<GetOneResult<TEntity>> GetAndUpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options);
        #endregion Find And Update
    }
}
