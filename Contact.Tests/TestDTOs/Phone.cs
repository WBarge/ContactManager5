using System;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Tests.TestDTOs
{
    internal class Phone : IPhone
    {
        public Guid PhoneId { get; set; }
        public string AreaCode { get; set; }
        public string CountryCode { get; set; }
        public string Number { get; set; }
        public string PhoneType { get; set; }
        public bool Deleted { get; set; }
    }
}