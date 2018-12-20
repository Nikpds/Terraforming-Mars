using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Terraforming.Api.Models;

namespace Terraforming.Api.Database
{
    public class MongoDbRepository<T> where T : Entity
    {
        protected internal IMongoDatabase db;
        protected internal IMongoCollection<T> collection;

        public IMongoCollection<T> Collection { get { return collection; } }

        public MongoDbRepository(IMongoDatabase database, string collectionName)
        {
            this.db = database;
            this.collection = database.GetCollection<T>(collectionName);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var documents = this.collection.Find(predicate);
            var result = documents.ToList();

            return result;
        }

        public virtual IEnumerable<T> Get(FilterDefinition<T> filter)
        {
            var documents = this.collection.Find(filter);
            var result = documents.ToList();
            return result;
        }

        public virtual T GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = collection.Find(filter).SingleOrDefault();

            return result;
        }

        public virtual IEnumerable<T> GetAll()
        {
            var documents = this.collection.Find(x => true);
            var result = documents.ToList();

            return result;
        }

        public virtual IEnumerable<T> GetPage(Expression<Func<T, bool>> predicate, int page, int pageSize, ProjectionDefinition<T> projection = null)
        {
            var documents = this.collection.Find(predicate)
                .Skip(page > 0 ? (page - 1) * pageSize : 0)
                .Limit(pageSize > 0 ? pageSize : 0);
            if (projection != null)
            {
                documents = documents.Project<T>(projection);
            }
            var result = documents.ToList();

            return result;
        }

        public virtual IEnumerable<T> GetPage(FilterDefinition<T> filter, int page, int pageSize, ProjectionDefinition<T> projection = null)
        {
            var documents = this.collection.Find(filter)
                .Skip(page > 0 ? (page - 1) * pageSize : 0)
                .Limit(pageSize > 0 ? pageSize : 0);
            if (projection != null)
            {
                documents = documents.Project<T>(projection);
            }
            var result = documents.ToList();
            return result;
        }

        public virtual T Insert(T entity)
        {
            entity.Updated = DateTime.UtcNow;

            collection.InsertOne(entity);

            return entity;
        }

        public virtual IEnumerable<T> InsertMany(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                return new List<T>().AsEnumerable();
            else
            {
                entities = entities.Select(x =>
                {
                    x.Updated = DateTime.UtcNow;
                    return x;
                });
                collection.InsertMany(entities);
                return entities;
            }
        }

        public virtual T Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", entity.Id);
            var current = collection.Find(filter).Single();
            entity.Updated = DateTime.UtcNow;
            var result = collection.ReplaceOne(filter, entity);

            return entity;

        }

        public virtual bool Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = collection.DeleteOne(filter);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public virtual bool Delete(T entity)
        {
            return this.Delete(entity.Id);
        }

        public virtual bool Delete(Expression<Func<T, bool>> predicate)
        {
            var result = collection.DeleteMany(predicate);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual bool DeleteAll()
        {
            var result = collection.DeleteMany(x => true);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            var count = this.collection.CountDocuments(predicate);
            return (int)count;
        }

        public virtual int Count(FilterDefinition<T> filter)
        {
            var count = this.collection.CountDocuments(filter);
            return (int)count;
        }
    }
}