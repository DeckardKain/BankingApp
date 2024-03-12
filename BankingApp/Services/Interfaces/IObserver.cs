using BankingApp.DTO.System;

namespace BankingApp.Services.Interfaces
{
    public interface IObserver
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(StateChangeDTO stateChange);
    }
}
