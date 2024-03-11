namespace BankingApp.Models.Accounts
{
    // Liskov Substitution - Instances of Retirement can be used interchangeably with instances of the Account class.
    // Open/Closed - New functionality related to Retirement can be added to the system by extending the Retirement class or by creating subclasses.
    // Single Responsibility - While there is no direct example of this, it is implied that this should only represent a Retirement Account.
    internal class Retirement : Account
    {
        // Open to Extension!
    }
}
