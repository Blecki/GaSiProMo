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

            var westHatch = GetObject("Hatch@OpeningAirlockA-A");
            var eastHatch = GetObject("Hatch@OpeningAirlockA-B");

            OpenLink(Direction.WEST, "Scenes.Opening.PassengerCabin", westHatch);
            OpenLink(Direction.EAST, "Scenes.Opening.ShaftA", eastHatch);

            var panel = new MudObject("cycle panel", "It is a small panel that cycles the airlock.");
            Move(panel, this);
            panel.CheckCanTake().Do((a, t) =>
            {
                SendMessage(a, "I can't take it, it's part of the wall.");
                return SharpRuleEngine.CheckResult.Disallow;
            });
            panel.MakeUseable();
   

            RMUD.Core.GlobalRules.Perform<RMUD.PossibleMatch, RMUD.Actor>("before acting")
                .When((match, actor) => object.ReferenceEquals(actor.Location, this))
                .When((match, actor) => object.ReferenceEquals(match["SUBJECT"], westHatch))
                .When((match, actor) => match.TypedValue<CommandEntry>("COMMAND").GetID() == "StandardActions:Open")
                .Do((match, actor) =>
                {
                    SendMessage(actor, "I need to get to the engine room. I can't go back to the passenger cabin.");
                    return SharpRuleEngine.PerformResult.Stop;
                });
        }
    }   
}