﻿using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Denifia.Stardew.SendItemsApi.Domain
{
    public sealed class Repository : IDisposable
    {
        private static Repository instance = null;
        private LiteRepository db;
        private const string connectionString = "data.db";
        // adding locking object
        private static readonly object syncRoot = new object();
        private Repository(bool initDb)
        {
            if (initDb)
            {
                db = new LiteRepository(connectionString);
            }
        }

        public static Repository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Repository(true);
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Insert a new document into collection. Document Id must be a new value in collection - Returns document Id
        /// </summary>
        public BsonValue Insert<T>(T entity, string collectionName = null)
        {
            return db.Insert<T>(entity, collectionName);
        }

        /// <summary>
        /// Insert an array of new documents into collection. Document Id must be a new value in collection. Can be set buffer size to commit at each N documents
        /// </summary>
        public int Insert<T>(IEnumerable<T> entities, string collectionName = null)
        {
            return db.Insert<T>(entities, collectionName);
        }

        /// <summary>
        /// Update a document into collection. Returns false if not found document in collection
        /// </summary>
        public bool Update<T>(T entity, string collectionName = null)
        {
            return db.Update<T>(entity, collectionName);
        }

        /// <summary>
        /// Update all documents
        /// </summary>
        public int Update<T>(IEnumerable<T> entities, string collectionName = null)
        {
            return db.Update<T>(entities, collectionName);
        }

        /// <summary>
        /// Insert or Update a document based on _id key. Returns true if insert entity or false if update entity
        /// </summary>
        public bool Upsert<T>(T entity, string collectionName = null)
        {
            return db.Upsert<T>(entity, collectionName);
        }

        /// <summary>
        /// Insert or Update all documents based on _id key. Returns entity count that was inserted
        /// </summary>
        public int Upsert<T>(IEnumerable<T> entities, string collectionName = null)
        {
            return db.Upsert<T>(entities, collectionName);
        }

        /// <summary>
        /// Delete entity based on _id key
        /// </summary>
        public bool Delete<T>(BsonValue id, string collectionName = null)
        {
            return db.Delete<T>(id, collectionName);
        }

        /// <summary>
        /// Delete entity based on Query
        /// </summary>
        public int Delete<T>(Query query, string collectionName = null)
        {
            return db.Delete<T>(query, collectionName);
        }

        /// <summary>
        /// Delete entity based on predicate filter expression
        /// </summary>
        public int Delete<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return db.Delete<T>(predicate, collectionName);
        }

        /// <summary>
        /// Returns new instance of LiteQueryable that provides all method to query any entity inside collection. Use fluent API to apply filter/includes an than run any execute command, like ToList() or First()
        /// </summary>
        public LiteQueryable<T> Query<T>(string collectionName = null)
        {
            return db.Query<T>(collectionName);
        }

        /// <summary>
        /// Search for a single instance of T by Id. Shortcut from Query.SingleById
        /// </summary>
        public T SingleById<T>(BsonValue id, string collectionName = null)
        {
            return db.SingleById<T>(id, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).ToList();
        /// </summary>
        public List<T> Fetch<T>(Query query = null, string collectionName = null)
        {
            return db.Fetch<T>(query, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).ToList();
        /// </summary>
        public List<T> Fetch<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return db.Fetch<T>(predicate, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).First();
        /// </summary>
        public T First<T>(Query query = null, string collectionName = null)
        {
            return db.First<T>(query, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).First();
        /// </summary>
        public T First<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return db.First<T>(predicate, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).FirstOrDefault();
        /// </summary>
        public T FirstOrDefault<T>(Query query = null, string collectionName = null)
        {
            return db.FirstOrDefault<T>(query, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).FirstOrDefault();
        /// </summary>
        public T FirstOrDefault<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return db.FirstOrDefault<T>(predicate, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).Single();
        /// </summary>
        public T Single<T>(Query query = null, string collectionName = null)
        {
            return db.Single<T>(query, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).Single();
        /// </summary>
        public T Single<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return db.Single<T>(predicate, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).SingleOrDefault();
        /// </summary>
        public T SingleOrDefault<T>(Query query = null, string collectionName = null)
        {
            return db.SingleOrDefault<T>(query, collectionName);
        }

        /// <summary>
        /// Execute Query[T].Where(query).SingleOrDefault();
        /// </summary>
        public T SingleOrDefault<T>(Expression<Func<T, bool>> predicate, string collectionName = null)
        {
            return db.SingleOrDefault<T>(predicate, collectionName);
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
            }
        }
    }
}
