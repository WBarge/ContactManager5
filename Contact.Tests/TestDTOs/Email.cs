using System;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Tests.TestDTOs
{
    internal class Email : IEmail
    {
        public Guid EmailId { get; set; }
        public Guid PersonId { get; set; }
        public string Address { get; set; }
        public bool Deleted { get; set; }
        public bool DefaultEmail { get; set; }
    }
}