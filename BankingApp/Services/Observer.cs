using BankingApp.DTO.System;
using BankingApp.Services.Interfaces;


namespace BankingApp.Services
{
    // Single Responsibility - Used to write the console state changes of the application.
    // Dependency Inversion -  The Observer class depends on the IObserver interface, adhering to DIP by depending on abstractions rather than concrete implementations.
    // However, it directly writes to the console, which could be seen as a dependency on a concrete logging mechanism, violating DIP to some extent.
    public class Observer : IObserver
    {
        // Unused but shown for completeness of the pattern
        private List<IObserver> _observers = new List<IObserver>();

        // Unused but shown for completeness of the pattern
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        // Unused but shown for completeness of the pattern
        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        // Using the Observer pattern as a simple log to the console for demonstration purposes only.
        public void Notify(StateChangeDTO stateChange)
        {
            Console.WriteLine($"Observer: " + stateChange.Message);
        }

    }
}
