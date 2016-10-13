using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ViewModels;
using MongoDB.Driver;
using MongoDB.Bson;
using DAL.Models;

namespace DAL
{
    class MongoDbRepository : IMongoDbRepository
    {

        private HDMongoContext _mongoDbContext = null;
        public MongoDbRepository(HDMongoContext mongoDbContext = null)
        {
            _mongoDbContext = mongoDbContext != null ? mongoDbContext : new HDMongoContext();
        }

        /// <summary>
        /// A generic AddOne method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Result> AddOne<TEntity>(TEntity item) where TEntity : class, new()
        {
            var res = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
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
        public async Task<long> Count<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
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
        public async Task<long> Count<TEntity>(string id) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await Count<TEntity>(filter);
        }

        /// <summary>
        /// a generic DeleteMany method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<Result> DeleteMany<TEntity>(IEnumerable<string> ids) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await DeleteMany<TEntity>(filter);
        }

        /// <summary>
        /// a generic delete many method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<Result> DeleteMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var deleteRes = await collection.DeleteManyAsync(filter);
                if (deleteRes.DeletedCount < 1)
                {
                    var ex = new Exception();
                    result.Message = "DeleteMany could not delete items";
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
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Result> DeleteOne<TEntity>(string id) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await DeleteOne<TEntity>(filter);
        }

        public async Task<Result> DeleteOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var deleteRes = await collection.DeleteOneAsync(filter);
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
        public async Task<bool> Exists<TEntity>(string id) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
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
        public IFindFluent<TEntity, TEntity> FindCursor<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var collection = GetCollection<TEntity>();
            var cursor = collection.Find(filter);
            return cursor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<GetManyResults<TEntity>> GetAll<TEntity>() where TEntity : class, new()
        {
            var res = new GetManyResults<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
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
        public async Task<GetOneResult<TEntity>> GetAndUpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TEntity> options) where TEntity : class, new()
        {
            var result = new GetOneResult<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
                result.Entity = await collection.FindOneAndUpdateAsync(filter, update, options);
                result.Success = true;
                result.Message = "OK";
                return result;
            }
            catch (Exception ex)
            {
                result.Message = NotifyException("GetAndUpdateOne", "Exception getting and updating one " + typeof(TEntity), ex);
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
        public async Task<GetManyResults<TEntity>> GetMany<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var res = new GetManyResults<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
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
        public async Task<GetManyResults<TEntity>> GetMany<TEntity>(IEnumerable<string> ids) where TEntity : class, new()
        {
            try
            {
                var collection = GetCollection<TEntity>();
                var filter = Builders<TEntity>.Filter.Eq("Id", ids);
                return await GetMany<TEntity>(filter);
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
        public async Task<GetOneResult<TEntity>> GetOne<TEntity>(string id) where TEntity : class, new()
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return await GetOne<TEntity>(filter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<GetOneResult<TEntity>> GetOne<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
        {
            var res = new GetOneResult<TEntity>();
            try
            {
                var collection = GetCollection<TEntity>();
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
        public async Task<Result> UpdateMany<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var updateRes = await collection.UpdateManyAsync(filter, update);
                if (updateRes.ModifiedCount < 1)
                {
                    var ex = new Exception();
                    result.Message = NotifyException("UpdateMany", "Exception updating many " + typeof(TEntity), ex);
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
        public async Task<Result> UpdateMany<TEntity>(IEnumerable<string> ids, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().In("Id", ids);
            return await UpdateMany<TEntity>(filter, update);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Result> UpdateOne<TEntity>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var result = new Result();
            try
            {
                var collection = GetCollection<TEntity>();
                var updateRes = await collection.UpdateOneAsync(filter, update);
                if (updateRes.ModifiedCount < 1)
                {
                    var ex = new Exception();
                    result.Message = NotifyException("UpdateOne", "Exception updating one " + typeof(TEntity), ex);
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
        public async Task<Result> UpdateOne<TEntity>(string id, UpdateDefinition<TEntity> update) where TEntity : class, new()
        {
            var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
            return await UpdateOne<TEntity>(filter, update);
        }

        /// <summary>
        /// private GetCollection function
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        private IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _mongoDbContext.GetCollection<TEntity>();
        }

        private string NotifyException(string functionName, string context, Exception ex)
        {
            string source = functionName + ": " + context;
            source = GetAllInfo(ex, source);
            Debug.WriteLine(source);
            return source;
        }

        private string GetAllInfo(Exception ex, string source)
        {
            var sb = new StringBuilder();
            sb.AppendLine("******** " + DateTime.Now.ToLongDateString() + " **********");

            while (ex != null)
            {
                sb.AppendLine("Inner Exception Type: ");
                sb.AppendLine(ex.InnerException.GetType().ToString());
                sb.AppendLine("Inner Exception: ");
                sb.AppendLine(ex.InnerException.Message);
                sb.AppendLine("Inner Source: ");
                sb.AppendLine(ex.InnerException.Source);
                if (ex.InnerException.StackTrace != null)
                {
                    sb.AppendLine("Inner Stack Trace: ");
                    sb.AppendLine(ex.InnerException.StackTrace);
                }
                sb.AppendLine("Exception Type: ");
                sb.AppendLine(sb.GetType().ToString());
                sb.AppendLine("Exception: " + ex.Message);
                sb.AppendLine("Source: " + source);
                sb.AppendLine("Stack Trace: ");
                if (ex.StackTrace != null)
                {
                    sb.AppendLine(ex.StackTrace);
                    sb.AppendLine();
                }
                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}
