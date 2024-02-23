using System;
using Contact.Glue.Interfaces.DTOs;
using Contact.Tests.TestDTOs;

namespace Contact.Tests.TestDataFactories
{
    internal static class PhoneFactory
    {
        internal static IPhone GeneratePhoneData()
        {
            return new Phone
            {
                PhoneId = Guid.NewGuid(),
                AreaCode = "213",
                CountryCode = "1",
                Number = "5553424",
                PhoneType = "cell",
                Deleted = false,
            };
        }
    }
}