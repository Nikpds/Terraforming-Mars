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

        public virtual async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate)
        {
            var documents = await this.collection.FindAsync(predicate);
            var result = await documents.ToListAsync();

            return result;
        }

        public virtual async Task<IEnumerable<T>> Get(FilterDefinition<T> filter)
        {
            var documents = await this.collection.FindAsync(filter);
            var result = await documents.ToListAsync();
            return result;
        }

        public virtual async Task<T> GetById(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.Find(filter).SingleOrDefaultAsync();

            return result;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var documents = await this.collection.FindAsync(x => true);
            var result = await documents.ToListAsync();

            return result;
        }

        public virtual async Task<IEnumerable<T>> GetPage(Expression<Func<T, bool>> predicate, int page, int pageSize, ProjectionDefinition<T> projection = null)
        {
            var documents = this.collection.Find(predicate)
                .Skip(page > 0 ? (page - 1) * pageSize : 0)
                .Limit(pageSize > 0 ? pageSize : 0);
            if (projection != null)
            {
                documents = documents.Project<T>(projection);
            }
            var result = await documents.ToListAsync();

            return result;
        }

        public virtual async Task<IEnumerable<T>> GetPage(FilterDefinition<T> filter, int page, int pageSize, ProjectionDefinition<T> projection = null)
        {
            var documents = this.collection.Find(filter)
                .Skip(page > 0 ? (page - 1) * pageSize : 0)
                .Limit(pageSize > 0 ? pageSize : 0);
            if (projection != null)
            {
                documents = documents.Project<T>(projection);
            }
            var result = await documents.ToListAsync();
            return result;
        }

        public virtual async Task<T> Insert(T entity)
        {
            entity.Updated = DateTime.UtcNow;

            await collection.InsertOneAsync(entity);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> InsertMany(IEnumerable<T> entities)
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
                await collection.InsertManyAsync(entities);
                return entities;
            }
        }

        public virtual async Task<T> Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq("Id", entity.Id);
            var current = await collection.Find(filter).SingleAsync();
            entity.Updated = DateTime.UtcNow;
            var result = await collection.ReplaceOneAsync(filter, entity);

            return entity;

        }

        public virtual async Task<bool> Delete(string id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.DeleteOneAsync(filter);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }

        public virtual async Task<bool> Delete(T entity)
        {
            return await this.Delete(entity.Id);
        }

        public virtual async Task<bool> Delete(Expression<Func<T, bool>> predicate)
        {
            var result = await collection.DeleteManyAsync(predicate);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual async Task<bool> DeleteAll()
        {
            var result = await collection.DeleteManyAsync(x => true);

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public virtual async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            var count = await this.collection.CountDocumentsAsync(predicate);
            return (int)count;
        }

        public virtual async Task<int> Count(FilterDefinition<T> filter)
        {
            var count = await this.collection.CountDocumentsAsync(filter);
            return (int)count;
        }
    }
}