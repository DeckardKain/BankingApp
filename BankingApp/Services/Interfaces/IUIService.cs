using static BankingApp.Services.UIService;

namespace BankingApp.Services.Interfaces
{
    public interface IUIService
    {
        event Action? OnUpdateUI;
        void UpdateUI();
    }
}
