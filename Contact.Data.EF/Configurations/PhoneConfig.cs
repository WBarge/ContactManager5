using Contact.Data.EF.ConcretePocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Data.EF.Configurations
{
    internal class PhoneConfig: IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("Phone");
            builder.HasKey(k => k.PhoneId);
            builder.Property(p => p.PhoneId)
                .IsRequired(true)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Deleted)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(p => p.CountryCode)
                .HasMaxLength(4);
            builder.Property(p => p.AreaCode)
                .HasMaxLength(4);
            builder.Property(p => p.Number)
                .HasMaxLength(8);

            //define many-many relationship
            builder.HasMany(phone => phone.People)
                .WithMany(person => person.Phones)
                .UsingEntity<MtmPhone>(
                    joinTable =>joinTable
                        .HasOne(mtmPhone=>mtmPhone.Person)
                        .WithMany(person=>person.MtmPhones)
                        .HasForeignKey(mtmPhone=>mtmPhone.PersonId)
                        .OnDelete(DeleteBehavior.NoAction),
                    joinTable=>joinTable
                        .HasOne(mtmPhone=>mtmPhone.Phone)
                        .WithMany(phone=>phone.MtmPhones)
                        .HasForeignKey(mtmPhone=>mtmPhone.PhoneId)
                        .OnDelete(DeleteBehavior.NoAction),
                    joinTable =>
                    {
                        joinTable.Property(pt => pt.DefaultNumber)
                            .IsRequired()
                            .HasDefaultValue(false);
                        joinTable.HasKey(k => new {k.PersonId, k.PhoneId});
                    });
        }
    }
}
