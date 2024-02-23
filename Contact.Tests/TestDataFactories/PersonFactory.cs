using System;
using Contact.Tests.TestDTOs;

namespace Contact.Tests.TestDataFactories
{
    internal static class PersonFactory
    {
        internal static Person GetPerson()
        {
            return new Person()
            {
                PersonId = Guid.NewGuid(),
                First = "foo",
                Last = "bar",
                Deleted = false
            };
        }
    }
}