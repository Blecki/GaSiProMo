using RMUD;
using StandardActionsModule;

namespace Space.Scenes.Opening
{

    public class AirlockA : Room
    {
        public override void Initialize()
        {
            Short = "Airlock";
            BriefDescription = "I'm in the airlock between the passenger cabin and the main shaft.";

            OpenLink(Direction.WEST, "Scenes.Opening.PassengerCabin", GetObject("Hatch@OpeningAirlockA-A"));
            OpenLink(Direction.EAST, "Scenes.Opening.ShaftA", GetObject("Hatch@OpeningAirlockA-B"));
        }
    }   
}