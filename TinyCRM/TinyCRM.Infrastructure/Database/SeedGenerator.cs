using Bogus;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infrastructure.Database
{
    public static class SeedGenerator
    {
        public static List<AccountEntity> GenerateAccounts()
        {
            var account = new Faker<AccountEntity>()
                .RuleFor(a => a.Id, f => f.Random.Guid())
                .RuleFor(a => a.Name, f => f.Person.FullName)
                .RuleFor(a => a.Email, f => f.Person.Email)
                .RuleFor(a => a.PhoneNumber, f => f.Person.Phone)
                .RuleFor(a => a.Address, f => f.Address.City())
            .RuleFor(a => a.ToSales, f => f.Random.Double(1, 100000));

            return account.Generate(10);
        }

        public static List<ContactEntity> GenerateContacts()
        {
            List<ContactEntity> contacts = new();

            var accounts = GenerateAccounts();

            foreach (var account in accounts)
            {
                var contact = new Faker<ContactEntity>()
                    .RuleFor(c => c.Id, f => f.Random.Guid())
                    .RuleFor(c => c.Name, f => f.Person.FullName)
                    .RuleFor(c => c.Email, f => f.Person.Email)
                    .RuleFor(c => c.PhoneNumber, f => f.Person.Phone)
                    .RuleFor(c => c.AccountId, f => account.Id);

                contacts.Add(contact.Generate());
            }
            return contacts;
        }

        //public static List<Lead> GenerateLeads()
        //{
        //    List<Lead> leads = new();

        //    var accounts = GenerateAccounts();

        //    foreach (var account in accounts)
        //    {
        //        var lead = new Faker<Lead>()
        //            .RuleFor(c => c.Id, f => f.Random.Guid())
        //            .RuleFor(c => c.Title, f => f.Random.Words())
        //            .RuleFor(c => c.CustomerId, f => account.Id)
        //            .RuleFor(c => c.Source, f => f.Random.Int(1, 5))

        //        leads.Add(lead.Generate());
        //    }
        //    return leads;
        //}
    }
}
