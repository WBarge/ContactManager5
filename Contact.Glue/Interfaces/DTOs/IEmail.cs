// ***********************************************************************
// Assembly         : 
// Author           : Admin
// Created          : 05-11-2019
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="IEmail.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace Contact.Glue.Interfaces.DTOs
{
    /// <summary>
    /// Represents a Email.
    /// NOTE: This interface is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public interface IEmail 
    {
        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>The email identifier.</value>
        Guid EmailId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        Guid PersonId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IEmail"/> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [default email].
        /// </summary>
        /// <value><c>true</c> if [default email]; otherwise, <c>false</c>.</value>
        bool DefaultEmail { get; set; }
    }
}
