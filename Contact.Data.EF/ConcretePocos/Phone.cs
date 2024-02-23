// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="Phone.cs" company="Contact.Data.EF">
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
    /// Class Phone.
    /// Implements the <see cref="Contact.Glue.Interfaces.DTOs.IPhone" />
    /// </summary>
    /// <seealso cref="Contact.Glue.Interfaces.DTOs.IPhone" />
    public class Phone : IPhone 
    {
        /// <summary>
        /// Gets or sets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        public Guid PhoneId { get; set; }
        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        /// <value>The area code.</value>
        public string AreaCode { get; set; }
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the type of the phone.
        /// </summary>
        /// <value>The type of the phone.</value>
        public string PhoneType { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Contact.Glue.Interfaces.DTOs.IPhone" /> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        public bool Deleted { get; set; }
        /// <summary>
        /// Gets or sets the people.
        /// </summary>
        /// <value>The people.</value>
        public ICollection<Person> People { get; set; }
        /// <summary>
        /// Gets or sets the MTM phones.
        /// </summary>
        /// <value>The MTM phones.</value>
        public List<MtmPhone> MtmPhones { get; set; }
    }
}
