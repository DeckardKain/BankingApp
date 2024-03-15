using Microsoft.AspNetCore.Components;

namespace BankingApp.Components.TabbedLayout
{
    public class Tab
    {
        public string Title { get; set; }
        public RenderFragment Content { get; set; }
    }
}
