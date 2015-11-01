using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Scenes.Opening
{
    public class Scene
    {
        public static void Start()
        {
            var ConversationCounter = 0;

            RMUD.Core.GlobalRules.Perform<RMUD.PossibleMatch, RMUD.Actor>("after acting")
                .ID("Opening-Scene-Rule")
                .Do((match, actor) =>
                {
                    var command = match["COMMAND"] as RMUD.CommandEntry;
                    if (command.GetID() == "Conversation:DiscussTopic")
                        ConversationCounter += 1;

                    if (ConversationCounter >= 2)
                        RMUD.MudObject.SendMessage(actor, "Scene end trigger");

                    return SharpRuleEngine.PerformResult.Continue;
                });
        }

        public static void End()
        {
            RMUD.Core.GlobalRules.DeleteRule("after acting", "Opening-Scene-Rule");
        }

        
    }
}
