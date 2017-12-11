using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace MobileArenaAPI.Data
{
    public class CollectionModel : TableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<HeroModel> Heroes { get; set; } = new List<HeroModel>();

    }
}
