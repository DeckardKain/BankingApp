namespace BankingAppBackEnd.Utilities
{
    public class UUIDGenerator
    {
        public static string GenerateUUID()
        {
            Guid uuid = Guid.NewGuid();

            string uuidString = uuid.ToString();

            return uuidString;
        }
    }
}
