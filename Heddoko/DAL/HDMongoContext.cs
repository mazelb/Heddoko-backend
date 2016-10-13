using System;
using System.Configuration;
using System.Collections.Generic;
using MongoDB.Driver;
using DAL.Models;

namespace DAL
{
    public class HDMongoContext
    {
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static HDMongoContext()
        {
            _client = new MongoClient();
            _database = _client.GetDatabase(Config.MongoDbName);
        }

        /// <summary>
        /// The private GetCollection method
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        /// 
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower() + "s");
        }
    }
}
