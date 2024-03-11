using BankingApp.Services.Interfaces;

namespace BankingApp.Services
{
    // Single Responsibility - Manage only UI-related functionality
    // Open/Closed - New UI-related features can be added without modifying the existing class. OnUpdateUI allows for extension by adding new subscribers without having to modify the class itself.
    // Dependency Inversion - Depends on IUIService, which is not a concrete implementation. This allows for flexibility in swapping out implementations at runtime.
    public class UIService : IUIService
    {
        public event Action? OnUpdateUI;
        
        // Trigger the event to all subscribed.
        public void UpdateUI()
        {
            OnUpdateUI?.Invoke();
        }
    }
}
