// ***********************************************************************
// Assembly         : 
// Author           : Admin
// Created          : 05-11-2019
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="IPerson.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Contact.Glue.Interfaces.DTOs
{
    /// <summary>
    /// Interface IPerson
    /// </summary>
    public interface IPerson 
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        Guid PersonId { get; set; }
        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        /// <value>The first.</value>
        string First { get; set; }
        /// <summary>
        /// Gets or sets the last.
        /// </summary>
        /// <value>The last.</value>
        string Last { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPerson"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        bool Deleted { get; set; }
    }
}
