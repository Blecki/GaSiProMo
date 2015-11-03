using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMUD;

namespace Space
{
    public class ControlPanel : MudObject
    {
        public enum IndicatorState
        {
            green,
            red
        }

        public IndicatorState Indicator = IndicatorState.green;

        public void UpdateHatchIndicatorState()
        {
            if (Location is Hatch)
            {
                var thisSide = FindLocale(this) as Room;
                var otherHatch = Portal.FindOppositeSide(Location);
                Room otherSide = otherHatch == null ? null : otherHatch.Location as Room;
                if (thisSide != null && otherSide != null && thisSide.AirLevel == otherSide.AirLevel)
                    Indicator = IndicatorState.green;
                else
                    Indicator = IndicatorState.red;
            }
        }

        public static void AtStartup(RuleEngine GlobalRules)
        {
            GlobalRules.Check<Actor, ControlPanel>("can take?")
                .Do((actor, panel) =>
                {
                    SendMessage(actor, "I can't take it. It's firmly attached.");
                    return SharpRuleEngine.CheckResult.Disallow;
                });

            GlobalRules.Perform<Actor, ControlPanel>("describe")
                .Do((actor, panel) =>
                {
                    panel.UpdateHatchIndicatorState();
                    SendMessage(actor, "It's a little square panel covered in buttons. There is a <s0> light on it.", panel.Indicator.ToString());
                    return SharpRuleEngine.PerformResult.Continue;
                });

            GlobalRules.Value<MudObject, ControlPanel, String, String>("printed name")
                .Do((viewer, panel, article) =>
                {
                    panel.UpdateHatchIndicatorState();
                    return String.Format("{0} control panel ({1})", article, panel.Indicator);
                });
        }

        public ControlPanel()
        {
            SimpleName("control panel", "controls");
        }
    }
}
