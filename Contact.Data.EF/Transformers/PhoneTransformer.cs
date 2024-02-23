using Contact.Data.EF.ConcretePocos;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Data.EF.ConcreteRepos
{
    public class PhoneTransformer
    {
        public static Phone TransForm(IPhone entity)
        {
            return new Phone
            {
                PhoneId = entity.PhoneId,
                AreaCode = entity.AreaCode,
                CountryCode = entity.CountryCode,
                Number = entity.Number,
                PhoneType = entity.PhoneType,
                Deleted = entity.Deleted
            };
        }
    }
}