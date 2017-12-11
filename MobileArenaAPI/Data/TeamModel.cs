using System.Collections.Generic;
using System.Drawing;

namespace MobileArenaAPI.Data
{
    public class TeamModel
    {
        public List<FormationModel> Formation { get; set; } = new List<FormationModel>
        {
            new FormationModel
            {
                Hero = new HeroModel(),
                Position = new Point(3,1)
            },
            new FormationModel
            {
                Hero = new HeroModel(),
                Position = new Point(3,3)
            },
            new FormationModel
            {
                Hero = new HeroModel(),
                Position = new Point(2,4)
            },
            new FormationModel
            {
                Hero = new HeroModel(),
                Position = new Point(0,6)
            }
        };
    }
}
