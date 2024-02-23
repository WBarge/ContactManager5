using System;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Tests.TestDTOs
{
    internal class Person : IPerson
    {
        public Guid PersonId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public bool Deleted { get; set; }
    }
}