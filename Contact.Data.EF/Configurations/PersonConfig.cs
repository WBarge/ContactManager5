using Contact.Data.EF.ConcretePocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact.Data.EF.Configurations
{
    internal class PersonConfig : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");
            builder.HasKey(k => k.PersonId);
            builder.Property(p => p.PersonId)
                .IsRequired(true)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.Deleted)
                .IsRequired()
                .HasDefaultValue(false);
            builder.Property(p => p.First)
                .HasMaxLength(80);
            builder.Property(p => p.Last)
                .IsRequired()
                .HasMaxLength(80);
            
            //set many-1 relationship
            builder.HasMany<Email>(person => person.Emails)
                .WithOne(email => email.Person);

            //set many-many relationship
            builder.HasMany(person => person.Phones)
                .WithMany(phone => phone.People)
                .UsingEntity<MtmPhone>(
                    jt => jt
                        .HasOne(mtmPhone => mtmPhone.Phone)
                        .WithMany(phone => phone.MtmPhones)
                        .HasForeignKey(mtmPhone => mtmPhone.PhoneId)
                        .OnDelete(DeleteBehavior.NoAction),
                    jt => jt
                        .HasOne(mtmPhone => mtmPhone.Person)
                        .WithMany(person => person.MtmPhones)
                        .HasForeignKey(mtmPhone => mtmPhone.PersonId)
                        .OnDelete(DeleteBehavior.NoAction),
                    jt =>
                    {
                        jt.Property(pt => pt.DefaultNumber)
                            .IsRequired()
                            .HasDefaultValue(false);
                        jt.HasKey(k => new {k.PersonId, k.PhoneId});
                    });
        }
    }
}
