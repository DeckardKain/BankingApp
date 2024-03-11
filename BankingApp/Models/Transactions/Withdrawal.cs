namespace BankingApp.Models.Transactions
{
    // Liskov Substitution - Instances of Withdrawal can be used interchangeably with instances of the Transaction class.
    // Open/Closed - New functionality related to Withdrawals can be added to the system by extending the Withdrawal class or by creating subclasses.
    // Single Responsibility - While there is no direct example of this, it is implied that this should only represent a Withdrawal Transaction.
    internal class Withdrawal : Transaction
    {
        // Open to Extension!
    }
}
