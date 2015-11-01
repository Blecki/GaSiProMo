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

                    SwitchPlayerCharacter(RMUD.MudObject.GetObject("Scenes.Opening.Player") as RMUD.Player);
                    RMUD.MudObject.Move(Player, RMUD.MudObject.GetObject("Scenes.Opening.PassengerCabin"));

                    RMUD.MudObject.SendMessage(Player, "  I'm in the passenger cabin of the Courageous Lion, a tiny private space liner with a grandiose name. We are three jumps from Sol. Not a bad distance, for my first interstellar trip. I am strapped tight into the flight couch as Sal drones on about safety procedures and protocols. Nevermind that we've done three jumps already, and only have one left. We will jump again in a few minutes, and all the passengers and crew need to be locked down when that happens. Everything on the ship will turn off, momentarily, including us.\n\n  There are three passengers beside myself, and Sal, in the cabin. My son, Daniel. The daughter of the one of the crewpersons. She is shy and won't talk to us. And an enourmous, gaudy, purple and yellow bird.");
        
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