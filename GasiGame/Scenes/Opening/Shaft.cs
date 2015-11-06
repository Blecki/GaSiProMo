using RMUD;
using StandardActionsModule;

namespace Space.Scenes.Opening
{

    public class Shaft : Room
    {
        public override void Initialize()
        {
            Short = "Ventral Shaft";
            BriefDescription = "I'm in the ventral shaft that runs down the back of the Courageous Lion.";

            Long = "I'm in the ventral shaft that runs down the back of the Courageous Lion. It makes a straight line between the passenger cabin and the engine room. Lights ring the coorridor every few feet. In between, there are stripes of deck grating, all the way around, so that no matter which way was down, I would have somewhere to walk.";

            OpenLink(Direction.FORE, "Scenes.Opening.AirlockA", GetObject("Hatch@Shaft-A"));
        }
    }   
}