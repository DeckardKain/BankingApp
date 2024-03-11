namespace BankingApp.Models.System
{
    // Single Responsibility - Immutable object to shuttle data between UI and Lower Layers.
    public record StateChangeDTO
    {
        public string? Message { get; set; }
    }

}
