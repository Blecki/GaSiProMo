using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space.Scenes.Opening
{
    public class OpeningScene
    {
        public static void Start()
        {
            var ConversationCounter = 0;

            Game.SwitchPlayerCharacter(RMUD.MudObject.GetObject("Scenes.Opening.Player") as RMUD.Player);
            RMUD.MudObject.Move(Game.Player, RMUD.MudObject.GetObject("Scenes.Opening.PassengerCabin"));

            RMUD.MudObject.SendMessage(Game.Player, 
@"I'm in the passenger cabin of the Courageous Lion, a tiny private space liner with a grandiose name. We are three jumps from Sol. Not a bad distance, for my first interstellar trip. I am strapped tight into the flight couch as Sal drones on about safety procedures and protocols. Nevermind that we've done three jumps already, and only have one left. We will jump again in a few minutes, and all the passengers and crew need to be locked down when that happens. Everything on the ship will turn off, momentarily, including us.

There are three passengers beside myself, and Sal, in the cabin. My son, Daniel; A young girl who I think may the daughter of one of the crewpersons; and an enourmous, gaudy, purple and yellow bird.

[You might try the command 'look' to look around at your surroundings, or 'examine bird' to take a closer look at something specific.]");

            RMUD.Core.GlobalRules.Perform<RMUD.PossibleMatch, RMUD.Actor>("before acting")
                .ID("Opening-Scene-Rule")
                .Do((match, actor) =>
                {
                    if (!Object.ReferenceEquals(actor, Game.Player))
                        return SharpRuleEngine.PerformResult.Continue;

                    var command = match["COMMAND"] as RMUD.CommandEntry;
                    if (!command.GetID().StartsWith("Conversation:"))
                    {

                        RMUD.MudObject.SendMessage(actor, "I'm strapped into a flight couch. I can't do that.");
                        return SharpRuleEngine.PerformResult.Stop;
                    }

                    return SharpRuleEngine.PerformResult.Continue;
                });

                    RMUD.Core.GlobalRules.Perform<RMUD.PossibleMatch, RMUD.Actor>("after acting")
                .ID("Opening-Scene-Rule")
                .Do((match, actor) =>
                {
                    if (!Object.ReferenceEquals(actor, Game.Player))
                        return SharpRuleEngine.PerformResult.Continue;

                    var command = match["COMMAND"] as RMUD.CommandEntry;
                    if (command.GetID().StartsWith("Conversation:"))
                    {
                        ConversationCounter += 1;
                        if (ConversationCounter >= 2)
                            End();
                    }
                    
                    return SharpRuleEngine.PerformResult.Continue;
                });
        }

        public static void End()
        {
            RMUD.Core.GlobalRules.DeleteRule("after acting", "Opening-Scene-Rule");
            RMUD.Core.GlobalRules.DeleteRule("before acting", "Opening-Scene-Rule");

            RMUD.MudObject.SendMessage(Game.Player, "  \"Jump,\" Sal says. Her only warning. The jump is an instant later. I feel a peculiar sensation, like my mind is separating from my body. I know it only takes the briefest of moments, but it stretches out in my mind. My limbs go numb. My vision blurs, then blackens, from the edges in. The girl is yelling something, but I have water in my ears. Then blackness.\n\n  When I wake, it's to fire coursing up and down my body. A thousand different tastes in my mouth, a cacaphony ringing in my ears. I can smell things I didn't know existed. My vision is a field of static. Slowly my nerves calm down. My senses return.\n\n  \"Dad! Dad!\" Daniel shouts beside me, shaking me.\n\n  I am the last to wake. Sal, long time spacer, shakes off the jump the fastest. She's holding a pair of fib paddles. The young recover quicker than the old, and both children are awake. The girl is sobbing quietly on her flight couch.\n\n  \"Bit slower than last time, Doc,\" Sal says while she stows the fib paddles. \"Don't think interstellar travel is going to be a prominent part of your future.\"\n\n  The jump past us, I free myself from the flight couch straps.");


            RMUD.Core.GlobalRules.Perform<RMUD.PossibleMatch, RMUD.Actor>("before acting")
                .ID("Opening-Scene-Rule")
                .Do((match, actor) =>
                {
                    if (!Object.ReferenceEquals(actor, Game.Player))
                        return SharpRuleEngine.PerformResult.Continue;

                    var command = match["COMMAND"] as RMUD.CommandEntry;
                    if (command.GetID().StartsWith("StandardActions:Open"))
                    {
                        RMUD.MudObject.SendMessage(Game.Player, "  Something shook the ship.\n\n  \"Grab onto something!\" Sal shouted.\n\n  The ship shook again, more violently this time. I grabbed the edge of the airlock as the ship rocked around me. Daniel was still in the couch. This wasn't normal turbulance. The pitch of Sal's voice told me that. Then the gravity cut out. A roar filled the ship. A chamber, somewhere, decompressing. Sal shouted something but I couldn't hear it over the incredible noise. She pulled a fire extinquisher from under my flight couch and sent it spinning through the air toward me.\n\n  At last the roar had passed. It hadn't been our chamber, at least, that decompressed. If it had, we'd be dead by now.\n\n  \"Engine room,\" Sal said. \"I'll get the kids in suits.\"\n\n  I nodded and punched the airlock controls. I'm no spacer, but in an emergency, everyone had something to do.");

                        RMUD.MudObject.Move(Game.Player, RMUD.MudObject.GetObject("Scenes.Opening.AirlockA"));
                        RMUD.Core.EnqueuActorCommand(Game.Player, "look");
                        RMUD.Core.GlobalRules.DeleteRule("before acting", "Opening-Scene-Rule");

                        return SharpRuleEngine.PerformResult.Stop;
                    }

                    return SharpRuleEngine.PerformResult.Continue;
                });
        }


    }
}
