﻿using System;
using MongoDB.Driver;

namespace DAL
{
    public class HDMongoContext
    {
        private static readonly Lazy<HDMongoContext> LazyContext = new Lazy<HDMongoContext>(() => new HDMongoContext());

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;

        public static HDMongoContext Instance => LazyContext.Value;

        private HDMongoContext()
            : this(new MongoClient(Config.MongoDbConnectionString), MongoUrl.Create(Config.MongoDbConnectionString).DatabaseName)
        {
        }

        private HDMongoContext(IMongoClient client, string dbName)
        {
            _client = client;
            _database = _client.GetDatabase(dbName);
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

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name)
        {
            return _database.GetCollection<TEntity>(name);
        }
    }
}