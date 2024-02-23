using System;
using Contact.Glue.Interfaces.DTOs;
using Contact.Tests.TestDTOs;

namespace Contact.Tests.TestDataFactories
{
    internal static class EmailFactory
    {
        internal static IEmail GenerateEmailData()
        {
            return new Email()
            {
                EmailId = Guid.NewGuid(),
                PersonId = Guid.NewGuid(),
                Address = @"SomeOne@gmail.com",
                Deleted = false,
                DefaultEmail = false
            };
        }
    }
}