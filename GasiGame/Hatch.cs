using RMUD;
using System;

namespace Space
{
    public class Hatch : Container
    {
        ControlPanel ControlPanel;

        public Hatch() : base(RelativeLocations.On, RelativeLocations.On)
        {
            Short = "hatch";
            Long = "It looks just like every other hatch.";

            this.Nouns.Add("HATCH");
            this.Nouns.Add("CLOSED", h => !GetBooleanProperty("open?"));
            this.Nouns.Add("OPEN", h => GetBooleanProperty("open?"));

            SetProperty("open?", false);
            SetProperty("openable?", true);

            Value<MudObject, Hatch, String, String>("printed name")
               .Do((viewer, hatch, article) =>
               {
                   hatch.ControlPanel.UpdateHatchIndicatorState();
                   var open = GetBooleanProperty("open?");
                   if (open) return String.Format("{0} open hatch", article);
                   else return String.Format("{0} closed hatch ({1})", article, hatch.ControlPanel.Indicator);
               });

            Check<MudObject, Hatch>("can open?")
                .Last
                .Do((a, b) =>
                {
                    if (GetBooleanProperty("open?"))
                    {
                        MudObject.SendMessage(a, "@already open");
                        return SharpRuleEngine.CheckResult.Disallow;
                    }
                    return SharpRuleEngine.CheckResult.Allow;
                })
                .Name("Can open doors rule.");

            Check<MudObject, Hatch>("can close?")
                .Last
                .Do((a, b) =>
                {
                    if (!GetBooleanProperty("open?"))
                    {
                        MudObject.SendMessage(a, "@already closed");
                        return SharpRuleEngine.CheckResult.Disallow;
                    }
                    return SharpRuleEngine.CheckResult.Allow;
                });

            Perform<MudObject, Hatch>("opened").Do((a, b) =>
            {
                SetProperty("open?", true);
                var otherSide = Portal.FindOppositeSide(this);
                if (otherSide != null) otherSide.SetProperty("open?", true);
                return SharpRuleEngine.PerformResult.Continue;
            });

            Perform<MudObject, Hatch>("close").Do((a, b) =>
            {
                SetProperty("open?", false);
                var otherSide = Portal.FindOppositeSide(this);
                if (otherSide != null) otherSide.SetProperty("open?", false);
                return SharpRuleEngine.PerformResult.Continue;
            });

            ControlPanel = new Space.ControlPanel();
            Move(ControlPanel, this);

            Check<Player, Hatch>("can open?")
                .Do((player, hatch) =>
                {
                    var thisSide = hatch.Location as Room;
                    var otherSide = Portal.FindOppositeSide(this).Location as Room;

                    if (otherSide == null || thisSide.AirLevel != otherSide.AirLevel)
                    {
                        SendMessage(player, "It won't open.");
                        return SharpRuleEngine.CheckResult.Disallow;
                    }

                    return SharpRuleEngine.CheckResult.Continue;
                });
        }
    }
}