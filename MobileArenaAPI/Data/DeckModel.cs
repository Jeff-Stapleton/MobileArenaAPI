using System;
using System.Collections.Generic;

namespace MobileArenaAPI.Data
{
    public class DeckModel
    {
        public int ActiveTeam { get; set; } = 0;
        public List<TeamModel> Teams { get; set; } = new List<TeamModel>(new TeamModel[] { new TeamModel() });
        public Guid Collection { get; set; }
    }
}
