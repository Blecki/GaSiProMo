using RMUD;
using StandardActionsModule;

namespace Space.Scenes.Opening
{

    public class PassengerCabin : Room
    {
        public override void Initialize()
        {
            Short = "Passenger Cabin";
            BriefDescription = "I'm in the passenger cabin again.";

            Long = "I'm in the passenger cabin of the Courageous Lion.";

            MudObject.Move(GetObject("Scenes.Opening.Sal"), this);
            MudObject.Move(GetObject("Scenes.Opening.Girl"), this);
            MudObject.Move(GetObject("Scenes.Opening.Daniel"), this);
            MudObject.Move(GetObject("Scenes.Opening.Bird"), this);

            OpenLink(Direction.AFT, "Scenes.Opening.AirlockA", GetObject("Hatch@OpeningPassengerCabin-A"));
        }
    }   
}