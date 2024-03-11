namespace BankingApp.Models.Transactions
{
    // Liskov Substitution - Instances of Deposit can be used interchangeably with instances of the Transaction class.
    // Open/Closed - New functionality related to desposits can be added to the system by extending the Deposit class or by creating subclasses.
    // Single Responsibility - While there is no direct example of this, it is implied that this should only represent a Deposit Transaction.
    internal class Deposit : Transaction
    {
        // Open to Extension!
    }
}
