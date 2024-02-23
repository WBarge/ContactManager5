// ***********************************************************************
// Assembly         : Contact.Data.EF
// Author           : Admin
// Created          : 03-27-2021
//
// Last Modified By : Admin
// Last Modified On : 03-27-2021
// ***********************************************************************
// <copyright file="MTMPhone.cs" company="Contact.Data.EF">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Contact.Data.EF.ConcretePocos
{
    /// <summary>
    /// Class MtmPhone.
    /// </summary>
    public class MtmPhone 
    {
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
        /// <summary>
        /// Gets or sets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        public Guid PhoneId { get; set; }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        public Phone Phone { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [default number].
        /// </summary>
        /// <value><c>true</c> if [default number]; otherwise, <c>false</c>.</value>
        public bool DefaultNumber { get; set; }
    }
}
