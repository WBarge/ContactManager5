// ***********************************************************************
// Assembly         : Contact.Service
// Author           : Bill Barge
// Created          : 02-13-2024
//
// Last Modified By : Bill Barge
// Last Modified On : 02-13-2024
// ***********************************************************************
// <copyright file="PhoneNumberRequest.cs" company="Contact.Service">
//     Copyright (c) N/A. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Newtonsoft.Json;

namespace Contact.Service.Models.Request
{
    /// <summary>
    /// Class PhoneNumberRequest.
    /// </summary>
    public class PhoneNumberRequest
    {
        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>The person identifier.</value>
        [JsonProperty(PropertyName = "personId")]
        public Guid PersonId { get; set; }
        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        /// <value>The area code.</value>
        [JsonProperty(PropertyName = "areaCode")]
        public string AreaCode { get; set; }
        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        [JsonProperty(PropertyName = "countryCode")]
        public string CountryCode { get; set; }
        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the type of the phone.
        /// </summary>
        /// <value>The type of the phone.</value>
        [JsonProperty(PropertyName = "phoneType")]
        public string PhoneType { get; set; }
    }
}