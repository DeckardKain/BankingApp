namespace BankingAppCore.Models.Accounts
{
    // Liskov Substitution - Instances of Savings can be used interchangeably with instances of the Account class.
    // Open/Closed - New functionality related to Savings can be added to the system by extending the Savings class or by creating subclasses.
    // Single Responsibility - While there is no direct example of this, it is implied that this should only represent a Savings Account.
    public class Savings : Account
    {
        // Open to Extension!
    }
}
