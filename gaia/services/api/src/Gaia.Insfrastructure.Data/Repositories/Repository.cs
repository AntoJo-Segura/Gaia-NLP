// <copyright file="Repository.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Dawn;
using Gaia.Core.Repositories;

namespace Gaia.Insfrastructure.Data.Repositories
{
    public abstract class Repository<T, TKey, TDynamoEntity> : IRepository<T, TKey>
        where T : class
    {
        public Repository(IAmazonDynamoDB client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        protected IAmazonDynamoDB Client { get; }

        public async Task<T> GetAsync(TKey id)
        {
            using (DynamoDBContext context = new DynamoDBContext(Client))
            {
                TDynamoEntity item = await context.LoadAsync<TDynamoEntity>(id);
                T entity = MapToEntity(item);

                return entity;
            }
        }

        public async Task InsertOrUpdateAsync(T obj)
        {
            using (DynamoDBContext context = new DynamoDBContext(Client))
            {
                TDynamoEntity dynamoEntity = MapToDynamoEntity(obj);

                await context.SaveAsync<TDynamoEntity>(dynamoEntity);
            }
        }

        public async Task BatchInsertAsync(IList<T> items)
        {
            Guard.Argument(items, nameof(items)).NotNull().NotEmpty();

            using (DynamoDBContext context = new DynamoDBContext(Client))
            {
                var objs = new List<TDynamoEntity>();

                foreach (var item in items)
                {
                    objs.Add(MapToDynamoEntity(item));
                }

                BatchWrite<TDynamoEntity> batch = context.CreateBatchWrite<TDynamoEntity>();
                batch.AddPutItems(objs);
                await batch.ExecuteAsync();
            }
        }

        protected abstract TDynamoEntity MapToDynamoEntity(T entity);

        protected abstract T MapToEntity(TDynamoEntity dynamoEntity);
    }
}
