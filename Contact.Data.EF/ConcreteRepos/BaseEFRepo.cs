// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="BaseEFRepo.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Contact.Data.EF.ConcreteRepos
{
    /// <summary>
    /// Class BaseEfRepo.
    /// This class is responsible for implementing base crud operations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEfRepo<T> where T:class
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>The database context.</value>
        protected ContactDbContext DbContext { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEfRepo{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <exception cref="System.ArgumentNullException">dbContext</exception>
        protected BaseEfRepo(ContactDbContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext) + " is mandatory!");
        }

        /// <summary>
        /// get all records as an asynchronous operation.
        /// </summary>
        /// <param name="maxRecordCount">The maximum record count.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List&lt;T&gt;.</returns>
        protected virtual async Task<List<T>> GetAllRecordsAsync(int maxRecordCount=int.MaxValue,CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().Take(maxRecordCount).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// get all records paged as an asynchronous operation.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List&lt;T&gt;.</returns>
        protected virtual async Task<List<T>> GetAllRecordsPagedAsync(int pageNumber = 1, int pageSize = 10,CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// find by condition as an asynchronous operation.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>List&lt;T&gt;.</returns>
        protected virtual async Task<List<T>> FindByConditionAsync(Expression<Func<T, bool>> expression,CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<T>().Where(expression).ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>T.</returns>
        protected virtual T Create() => Activator.CreateInstance<T>();

        /// <summary>
        /// insert as an asynchronous operation.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual async Task InsertAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        protected virtual void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// save as an asynchronous operation.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        protected async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
