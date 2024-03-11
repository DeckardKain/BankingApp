namespace BankingApp.Models.Accounts
{
    // Liskov Substitution - Instances of Chequing can be used interchangeably with instances of the Account class.
    // Open/Closed - New functionality related to Chequing can be added to the system by extending the Chequing class or by creating subclasses.
    // Single Responsibility - While there is no direct example of this, it is implied that this should only represent a Chequing Account.
    internal class Chequing : Account
    {
        // Open to Extension!
    }
}
