using System;
using System.Collections.Generic;

namespace MobileArenaAPI.Data
{
    public class HeroModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public int Hitpoints { get; set; }
        public List<string> Abilities { get; set; } = new List<string>();
    }
}
