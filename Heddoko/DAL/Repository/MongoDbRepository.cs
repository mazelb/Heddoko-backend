using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using DAL.Models;

namespace DAL
{
    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : class, new()
    {
        protected readonly HDMongoContext MongoDbContext;

        public MongoDbRepository(HDMongoContext mongoDbContext = null)
        {
            MongoDbContext = mongoDbContext ?? HDMongoContext.Instance;
        }

        #region GenericFunctions

        /// <summary>
        /// A generic AddOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Result> AddOne(TEntity item)
        {
            var res = new Result();
            try
            {
                var collection = GetCollection();
                await collection.InsertOneAsync(item);
                res.Success = true;
                res.Message = "OK";
                return res;
            }
            catch (Exception ex)
            {
                res.Message = NotifyException("AddOne", "Exception adding one " + typeof(TEntity), ex);
                return res;
            }
        }

        /// <summary>
        /// A generic Count method with filter
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> Count(FilterDefinition<TEntity> filter)
        {
            var collection = GetCollection();
            var cursor = collection.Find(filter);
            var count = await cursor.CountAsync();
            return count;
        }

        /// <summary>
        /// a generic count method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<long> Count(string id)
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await Count(filter);
        }

        /// <summary>
        /// a generic DeleteMany method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<Result> DeleteMany(IEnumerable<string> ids)
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await DeleteMany(filter);
        }

        /// <summary>
        /// a generic delete many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<Result> DeleteMany(FilterDefinition<TEntity> filter)
        {
            var result = new Result();
            try
            {
                var collection = GetCollection();
                var deleteRes = await collection.DeleteManyAsync(filter);
                if (deleteRes.DeletedCount < 1)
                {
                    result.Message = NotifyErrorMessage("DeleteMany", "Could not delete many items " + typeof(TEntity));
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = NotifyException("DeleteMany", "Exception deleting many " + typeof(TEntity), ex);
                result.ErrorCode = 600;
                return result;
            }
        }

        /// <summary>
        /// A generic DeleteOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> DeleteOne(string id)
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await DeleteOne(filter);
        }

        public async Task<Result> DeleteOne(FilterDefinition<TEntity> filter)
        {
            var result = new Result();
            try
            {
                var collection = GetCollection();
                DeleteResult deleteRes = await collection.DeleteOneAsync(filter);
                if (deleteRes.DeletedCount < 1)
                {
                    result.Message = NotifyErrorMessage("DeleteOne", "Could not delete one " + typeof(TEntity));
                    result.ErrorCode = 600;
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = NotifyException("DeleteOne", "Exception deleting one " + typeof(TEntity), ex);
                result.ErrorCode = 600;
                return result;
            }
        }

        /// <summary>
        /// A generic Exists method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Exists(string id)
        {
            var collection = GetCollection();
            var query = new BsonDocument("Id", id);
            var cursor = collection.Find(query);
            var count = await cursor.CountAsync();
            return (count > 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IFindFluent<TEntity, TEntity> FindCursor(FilterDefinition<TEntity> filter)
        {
            var collection = GetCollection();
            var cursor = collection.Find(filter);
            return cursor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<GetManyResults<TEntity>> GetAll()
        {
            var res = new GetManyResults<TEntity>();
            try
            {
                var collection = GetCollection();
                var entities = await collection.Find(new BsonDocument()).ToListAsync();
                if (entities != null)
                {
                    res.Entities = entities;
                }
                res.Success = true;
                res.Message = "OK";
                return res;
            }
            catch (Exception ex)
            {
                res.Message = NotifyException("GetAll", "Exception getting all " + typeof(TEntity), ex);
                res.ErrorCode = 600;
                return res;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<GetOneResult<TEntity>> GetAndUpdateOne(FilterDefinition<TEntity> filter,
            UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options)
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                var collection = GetCollection();
                result.Entity = await collection.FindOneAndUpdateAsync(filter, update, options);
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = NotifyException("GetAndUpdateOne",
                    "Exception getting and updating one " + typeof(TEntity), ex);
                result.ErrorCode = 600;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<GetManyResults<TEntity>> GetMany(FilterDefinition<TEntity> filter)
        {
            var res = new GetManyResults<TEntity>();
            try
            {
                var collection = GetCollection();
                var entities = await collection.Find(filter).ToListAsync();
                if (entities != null)
                {
                    res.Entities = entities;
                }
                res.Success = true;
                res.Message = "OK";
                return res;
            }
            catch (Exception ex)
            {
                res.Message = NotifyException("GetMany", "Exception getting many " + typeof(TEntity), ex);
                res.ErrorCode = 600;
                return res;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<GetManyResults<TEntity>> GetMany(IEnumerable<string> ids)
        {
            try
            {
                var filter = Builders<TEntity>.Filter.Eq("Id", ids);
                return await GetMany(filter);
            }
            catch (Exception ex)
            {
                var res = new GetManyResults<TEntity>();
                res.Message = NotifyException("GetMany", "Exception getting many " + typeof(TEntity), ex);
                res.ErrorCode = 600;
                return res;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GetOneResult<TEntity>> GetOne(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return await GetOne(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<GetOneResult<TEntity>> GetOne(FilterDefinition<TEntity> filter)
        {
            var res = new GetOneResult<TEntity>();
            try
            {
                var collection = GetCollection();
                var entity = await collection.Find(filter).SingleOrDefaultAsync();
                if (entity != null)
                {
                    res.Entity = entity;
                }
                res.Success = true;
                res.Message = "OK";
                return res;
            }
            catch (Exception ex)
            {
                res.Message = NotifyException("GetOne", "Exception getting one " + typeof(TEntity), ex);
                res.ErrorCode = 600;
                return res;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Result> UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            var result = new Result();
            try
            {
                var collection = GetCollection();
                var updateRes = await collection.UpdateManyAsync(filter, update);
                if (updateRes.ModifiedCount < 1)
                {
                    result.Message = NotifyErrorMessage("UpdateMany", "Could not update many " + typeof(TEntity));
                    result.ErrorCode = 600;
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = NotifyException("UpdateMany", "Exception updating many " + typeof(TEntity), ex);
                result.ErrorCode = 600;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Result> UpdateMany(IEnumerable<string> ids, UpdateDefinition<TEntity> update)
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await UpdateMany(filter, update);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Result> UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            var result = new Result();
            try
            {
                var collection = GetCollection();
                var updateRes = await collection.UpdateOneAsync(filter, update);
                if (updateRes.ModifiedCount < 1)
                {
                    result.Message = NotifyErrorMessage("UpdateOne", "Could not update one " + typeof(TEntity));
                    result.ErrorCode = 600;
                    return result;
                }
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = NotifyException("UpdateOne", "Exception updating one " + typeof(TEntity), ex);
                result.ErrorCode = 600;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Result> UpdateOne(string id, UpdateDefinition<TEntity> update)
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await UpdateOne(filter, update);
        }

        /// <summary>
        /// private GetCollection function
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual IMongoCollection<TEntity> GetCollection()
        {
            return MongoDbContext.GetCollection<TEntity>();
        }

        #endregion

        #region PrivateFunctions

        private string NotifyErrorMessage(string functionName, string context)
        {
            return NotifyException(functionName, context, null);
        }

        private string NotifyException(string functionName, string context, Exception ex)
        {
            string source = "MongoDbRepository." + functionName + ": " + context;
            source = GetAllInfo(ex, source);

            Trace.TraceError(source);

            return source;
        }

        private string GetAllInfo(Exception ex, string source)
        {
            var sb = new StringBuilder(source);

            if (ex != null)
            {
                sb.AppendLine(ex.GetBaseException().ToString());
            }

            return sb.ToString();
        }

        #endregion
    }
}
