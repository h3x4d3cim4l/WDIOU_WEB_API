namespace WDIOU_WEB_API.Models
{
    public class WDIOUDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string usedEmailsCollectionName { get; set; } = null!;
        public string DebtsCollectionName { get; set; } = null!;
        public string PersonCollectionName { get; set; } = null!;
    }
}
