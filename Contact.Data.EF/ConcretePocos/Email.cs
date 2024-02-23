// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="Email.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Data.EF.ConcretePocos
{
    /// <summary>
    /// Class Email.
    /// Implements the <see cref="Contact.Glue.Interfaces.DTOs.IEmail" />
    /// </summary>
    /// <seealso cref="Contact.Glue.Interfaces.DTOs.IEmail" />
    public class Email : IEmail 
    {
        /// <summary>
        /// Gets or sets the email identifier.
        /// </summary>
        /// <value>The email identifier.</value>
        public Guid EmailId { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Contact.Glue.Interfaces.DTOs.IEmail" /> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        public bool Deleted { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [default email].
        /// </summary>
        /// <value><c>true</c> if [default email]; otherwise, <c>false</c>.</value>
        public bool DefaultEmail { get; set; }
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        public Guid PersonId { get; set; }
        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>The person.</value>
        public Person Person { get; set; }
    }
}
