// ***********************************************************************
// Assembly         : 
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="IPeopleService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Glue.Interfaces.Services
{
    /// <summary>
    /// Interface IPeopleService
    /// </summary>
    public interface IPeopleService
    {
        /// <summary>
        /// Gets all people asynchronous.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;IEnumerable&lt;IPerson&gt;&gt;.</returns>
        Task<IEnumerable<IPerson>> GetAllPeopleAsync(CancellationToken token= default);
    }
}