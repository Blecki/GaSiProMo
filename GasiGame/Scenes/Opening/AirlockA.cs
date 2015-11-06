using RMUD;
using StandardActionsModule;
using SharpRuleEngine;

namespace Space.Scenes.Opening
{

    public class AirlockA : Room
    {
        private void AutoClose(MudObject Player, Hatch Hatch)
        {
            if (Hatch.GetBooleanProperty("open?"))
            {
                Hatch.SetProperty("open?", false);
                var otherSide = Portal.FindOppositeSide(Hatch);
                if (otherSide != null) otherSide.SetProperty("open?", false);
                MudObject.SendMessage(Player, "^<the0> closes.", Hatch);
            }
        }

        public override void Initialize()
        {
            Short = "Airlock";
            BriefDescription = "I'm in the airlock between the passenger cabin and the main shaft.";

            var foreHatch = GetObject("Hatch@OpeningAirlockA-A") as Hatch;
            var aftHatch = GetObject("Hatch@OpeningAirlockA-B") as Hatch;

            OpenLink(Direction.FORE, "Scenes.Opening.PassengerCabin", foreHatch);
            OpenLink(Direction.AFT, "Scenes.Opening.Shaft", aftHatch);

            var panel = new MudObject("cycle panel", "It is a small panel that cycles the airlock. All I have to do is USE it, and it will equalize the pressure in the airlock.");
            Move(panel, this);
            panel.CheckCanTake().Do((a, t) =>
            {
                SendMessage(a, "I can't take it, it's part of the wall.");
                return SharpRuleEngine.CheckResult.Disallow;
            });
            panel.MakeUseable();

            panel.PerformUsed().Do((actor, thisPanel) =>
            {
                AutoClose(actor, foreHatch);
                AutoClose(actor, aftHatch);

                var foreRoom = Portal.FindOppositeSide(foreHatch).Location as Room;
                var aftRoom = Portal.FindOppositeSide(aftHatch).Location as Room;

                var oldState = this.AirLevel;
                if (this.AirLevel != foreRoom.AirLevel)
                    this.AirLevel = foreRoom.AirLevel;
                else
                    this.AirLevel = aftRoom.AirLevel;

                if (this.AirLevel == oldState)
                {
                    if (this.AirLevel == AirLevel.Atmosphere)
                        SendMessage(actor, "The airlock remains pressurized.");
                    else
                        SendMessage(actor, "The airlock remains unpressurized.");
                }
                else
                {
                    if (this.AirLevel == AirLevel.Atmosphere)
                        SendMessage(actor, "The airlock is now pressurized.");
                    else
                        SendMessage(actor, "The airlock is not unpressurized.");
                }

                return SharpRuleEngine.PerformResult.Continue;
            });
   

            RMUD.Core.GlobalRules.Perform<RMUD.PossibleMatch, RMUD.Actor>("before acting")
                .When((match, actor) => object.ReferenceEquals(actor.Location, this))
                .When((match, actor) => match.ContainsKey("SUBJECT") && object.ReferenceEquals(match["SUBJECT"], foreHatch))
                .When((match, actor) => match.TypedValue<CommandEntry>("COMMAND").GetID() == "StandardActions:Open")
                .Do((match, actor) =>
                {
                    SendMessage(actor, "I need to get to the engine room. I can't go back to the passenger cabin.");
                    return SharpRuleEngine.PerformResult.Stop;
                });
        }
    }   
}