using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    public static class Game
    {
        public static RMUD.SinglePlayer.Driver Driver { get; set; }
        internal static RMUD.Player Player { get { return Driver.Player; } }

        public static void SwitchPlayerCharacter(RMUD.Player NewCharacter)
        {
            Driver.SwitchPlayerCharacter(NewCharacter);
        }

        public static void AtStartup(RMUD.RuleEngine GlobalRules)
        {
            GlobalRules.Perform<Player>("singleplayer game started")
                .First
                .Do((actor) =>
                {
                    // Don't list the things an actor is holding. This gets rid of all the spurious 'is empty handed' messages.
                    GlobalRules.DeleteRule("describe", "list-actor-held-items-rule");

                    Scenes.Opening.OpeningScene.Start();
                    
                    return SharpRuleEngine.PerformResult.Stop;
                });
        }

        public static void StartConversation(RMUD.Actor Actor, RMUD.MudObject NPC)
        {
            Actor.SetProperty("interlocutor", NPC);
            RMUD.Core.EnqueuActorCommand(Actor, "topics");
        }
    }
}