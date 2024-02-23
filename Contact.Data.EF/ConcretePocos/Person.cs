// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="Person.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Data.EF.ConcretePocos
{
    /// <summary>
    /// Class Person.
    /// Implements the <see cref="Contact.Glue.Interfaces.DTOs.IPerson" />
    /// </summary>
    /// <seealso cref="Contact.Glue.Interfaces.DTOs.IPerson" />
    public class Person : IPerson 
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        public Guid PersonId { get; set; }
        /// <summary>
        /// Gets or sets the first.
        /// </summary>
        /// <value>The first.</value>
        public string First { get; set; }
        /// <summary>
        /// Gets or sets the last.
        /// </summary>
        /// <value>The last.</value>
        public string Last { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Contact.Glue.Interfaces.DTOs.IPerson" /> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        public bool Deleted { get; set; }
        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        /// <value>The emails.</value>
        public ICollection<Email> Emails { get; set; }
        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        /// <value>The phones.</value>
        public ICollection<Phone> Phones { get; set; }
        /// <summary>
        /// Gets or sets the MTM phones.
        /// </summary>
        /// <value>The MTM phones.</value>
        public List<MtmPhone> MtmPhones { get; set; }
    }
}
