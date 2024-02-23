// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Admin
// Created          : 03-28-2021
//
// Last Modified By : Admin
// Last Modified On : 03-28-2021
// ***********************************************************************
// <copyright file="EmailRequest.cs" company="Contact.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace Contact.Service.Models.Request
{
    /// <summary>
    /// Class EmailRequest.
    /// </summary>
    public class AddEmailRequest
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        [JsonProperty(PropertyName = "personId")]
        public Guid PersonId { get; set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }
    }
}