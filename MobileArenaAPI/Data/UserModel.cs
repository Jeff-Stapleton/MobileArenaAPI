using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace MobileArenaAPI.Data
{
    public class UserModel : TableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public int Rank { get; set; } = 0;
        public DeckModel Deck { get; set; } = new DeckModel();
    }
}
