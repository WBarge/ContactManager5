using Contact.Data.EF.ConcretePocos;
using Contact.Glue.Interfaces.DTOs;

namespace Contact.Data.EF.Transformers
{
    public static class PersonTransformer
    {
        public static Person TransForm(IPerson entity)
        {
            return new Person
            {
                PersonId = entity.PersonId, 
                First = entity.First, 
                Last = entity.Last, 
                Deleted = entity.Deleted
            };
        }
    }
}