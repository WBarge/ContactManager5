using System;
using Newtonsoft.Json;

namespace Contact.Service.Models.Result
{
    /// <summary>
    /// Class PhoneResult.
    /// </summary>
    public class PhoneResult
    {
        /// <summary>
        /// Gets or sets the phone identifier.
        /// </summary>
        /// <value>The phone identifier.</value>
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }
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
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:Contact.Glue.Interfaces.DTOs.IPhone" /> is deleted.
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }
    }
}