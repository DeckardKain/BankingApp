using BankingAppCore.DTO.Account;
using BankingAppCore.DTO.UI;
using BankingAppCore.Models.Accounts;
using Bogus;

namespace BankingAppBackEnd.Utilities
{
    public class ObjectGenerator
    {
        private static readonly Randomizer _randomizer = new Randomizer();

        public static UserRegisterDTO GenerateUserRegisterObject()
        {
            var faker = new Faker<UserRegisterDTO>()
                .RuleFor(o => o.Name, f => f.Name.FullName())
                .RuleFor(o => o.Email, f => f.Internet.Email())
                .RuleFor(o => o.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(o => o.Address, f => f.Address.FullAddress())
                .RuleFor(o => o.Username, f => f.Internet.UserName())
                .RuleFor(o => o.Password, f => f.Internet.Password());

            return faker.Generate();
        }

        public static AccountDTO GenerateRandomAccount(string customerId)
        {
            var faker = new Faker<AccountDTO>()
                .RuleFor(a => a.Id, f => f.Random.Guid().ToString())
                .RuleFor(a => a.CustomerId, f => customerId)
                .RuleFor(a => a.AccountType, f => f.PickRandom<AccountType>())
                .RuleFor(a => a.Balance, f => f.Finance.Amount());

            return faker.Generate();
        }

    }
}
