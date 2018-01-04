using Microsoft.WindowsAzure.Storage.Table;

namespace MobileArenaAPI.Data
{
    public class CredentialsModel : TableEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserId { get; set; }
    }
}
