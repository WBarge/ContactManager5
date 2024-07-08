// ***********************************************************************
// Assembly         : Contact.Glue
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="IPersonRepo.cs" company="Contact.Glue">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;
#nullable enable

namespace Contact.Glue.Interfaces.Repos
{
    /// <summary>
    /// Interface IPersonRepo
    /// </summary>
    public interface IPersonRepo
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IPerson&gt;&gt;.</returns>
        Task<IEnumerable<IPerson>> GetAllAsync(CancellationToken token = default);
        /// <summary>
        /// Gets the person asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;System.Nullable&lt;IPerson&gt;&gt;.</returns>
        Task<IPerson?> GetPersonAsync(Guid id, CancellationToken token = default);
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task&lt;Guid&gt;.</returns>
        Task<Guid> InsertAsync(IPerson entity,CancellationToken cancellationToken = default);
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(IPerson entity, CancellationToken cancellationToken);
        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(IPerson entity, CancellationToken cancellationToken = default);
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>IPerson.</returns>
        IPerson Create();
    }
}