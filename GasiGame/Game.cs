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
            OverrideMessages();

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

        private static bool FirstConversation = true;

        public static void StartConversation(RMUD.Actor Actor, RMUD.MudObject NPC)
        {
            if (FirstConversation)
            {
                FirstConversation = false;
                RMUD.MudObject.SendMessage(Actor, "\n[You've entered into a conversation. During a conversation, you will be prompted with a list of possible topics. You can see the list again by entering the command 'topics'. You can enter into a conversation with any character at any time using 'greet &lt;character&gt;']");
            }

            Actor.SetProperty("interlocutor", NPC);
            RMUD.MudObject.SendMessage(Actor, "\n");
            RMUD.Core.EnqueuActorCommand(Actor, "topics");
        }

        public static void OverrideMessages()
        {
            //RMUD.Core.OverrideMessage("not here", "I don't see that here.");
            //RMUD.Core.OverrideMessage("gone", "The doesn't seem to be here any more.");
            //RMUD.Core.OverrideMessage("dont have that", "You don't have that.");
            //RMUD.Core.OverrideMessage("already have that", "You already have that.");
            //RMUD.Core.OverrideMessage("does nothing", "That doesn't seem to do anything.");
            //RMUD.Core.OverrideMessage("nothing happens", "Nothing happens.");
            //RMUD.Core.OverrideMessage("unappreciated", "I don't think <the0> would appreciate that.");
            //RMUD.Core.OverrideMessage("already open", "It is already open.");
            //RMUD.Core.OverrideMessage("already closed", "It is already closed.");
            //RMUD.Core.OverrideMessage("close it first", "You'll have to close it first.");
            //RMUD.Core.OverrideMessage("wrong key", "That is not the right key.");
            //RMUD.Core.OverrideMessage("error locked", "It seems to be locked.");
            //RMUD.Core.OverrideMessage("you close", "You close <the0>.");
            //RMUD.Core.OverrideMessage("they close", "^<the0> closes <the1>.");
            //RMUD.Core.OverrideMessage("you drop", "You drop <the0>.");
            //RMUD.Core.OverrideMessage("they drop", "^<the0> drops <a1>.");
            //RMUD.Core.OverrideMessage("is open", "^<the0> is open.");
            //RMUD.Core.OverrideMessage("is closed", "^<the0> is closed.");
            //RMUD.Core.OverrideMessage("describe on", "On <the0> is <l1>.");
            //RMUD.Core.OverrideMessage("describe in", "In <the0> is <l1>.");
            //RMUD.Core.OverrideMessage("empty handed", "^<the0> is empty handed.");
            //RMUD.Core.OverrideMessage("holding", "^<the0> is holding <l1>.");
            //RMUD.Core.OverrideMessage("dont see that", "I don't see that here.");
            //RMUD.Core.OverrideMessage("can't push direction", "That isn't going to work.");
            //RMUD.Core.OverrideMessage("you push", "You push <the0> <s1>.");
            //RMUD.Core.OverrideMessage("they push", "^<the0> pushed <the1> <s2>.");
            //RMUD.Core.OverrideMessage("they arrive pushing", "^<the0> arrives <s2> pushing <the1>.");
            //RMUD.Core.OverrideMessage("unmatched cardinal", "What way was that?");
            //RMUD.Core.OverrideMessage("go to null link", "You can't go that way.");
            //RMUD.Core.OverrideMessage("go to closed door", "The door is closed.");
            //RMUD.Core.OverrideMessage("you went", "You went <s0>.");
            //RMUD.Core.OverrideMessage("they went", "^<the0> went <s1>.");
            //RMUD.Core.OverrideMessage("bad link", "Error - Link does not lead to a room.");
            //RMUD.Core.OverrideMessage("they arrive", "^<the0> arrives <s1>.");
            //RMUD.Core.OverrideMessage("first opening", "[first opening <the0>]");
            //RMUD.Core.OverrideMessage("carrying", "You are carrying..");
            //RMUD.Core.OverrideMessage("not lockable", "I don't think the concept of 'locked' applies to that.");
            //RMUD.Core.OverrideMessage("you lock", "You lock <the0>.");
            //RMUD.Core.OverrideMessage("they lock", "^<the0> locks <the1> with <the2>.");
            //RMUD.Core.OverrideMessage("nowhere", "You aren't anywhere.");
            //RMUD.Core.OverrideMessage("dark", "It's too dark to see.");
            //RMUD.Core.OverrideMessage("also here", "Also here: <l0>.");
            //RMUD.Core.OverrideMessage("on which", "(on which is <l0>)");
            //RMUD.Core.OverrideMessage("obvious exits", "Obvious exits:");
            //RMUD.Core.OverrideMessage("through", "through <the0>");
            //RMUD.Core.OverrideMessage("to", "to <the0>");
            //RMUD.Core.OverrideMessage("cant look relloc", "You can't look <s0> that.");
            //RMUD.Core.OverrideMessage("is closed error", "^<the0> is closed.");
            //RMUD.Core.OverrideMessage("relloc it is", "^<s0> <the1> is..");
            //RMUD.Core.OverrideMessage("nothing relloc it", "There is nothing <s0> <the1>.");
            //RMUD.Core.OverrideMessage("not openable", "I don't think the concept of 'open' applies to that.");
            //RMUD.Core.OverrideMessage("you open", "You open <the0>.");
            //RMUD.Core.OverrideMessage("they open", "^<the0> opens <the1>.");
            //RMUD.Core.OverrideMessage("cant put relloc", "You can't put things <s0> that.");
            //RMUD.Core.OverrideMessage("you put", "You put <the0> <s1> <the2>.");
            //RMUD.Core.OverrideMessage("they put", "^<the0> puts <the1> <s2> <the3>.");
            //RMUD.Core.OverrideMessage("say what", "Say what?");
            //RMUD.Core.OverrideMessage("emote what", "You exist. Actually this is an error message, but that's what you just told me to say.");
            //RMUD.Core.OverrideMessage("speak", "^<the0> : " < s1 > "");
            //RMUD.Core.OverrideMessage("emote", "^<the0> <s1>");
            //RMUD.Core.OverrideMessage("you take", "You take <the0>.");
            //RMUD.Core.OverrideMessage("they take", "^<the0> takes <the1>.");
            //RMUD.Core.OverrideMessage("cant take people", "You can't take people.");
            //RMUD.Core.OverrideMessage("cant take portals", "You can't take portals.");
            //RMUD.Core.OverrideMessage("cant take scenery", "That's a terrible idea.");
            //RMUD.Core.OverrideMessage("you unlock", "You unlock <the0>.");
            //RMUD.Core.OverrideMessage("they unlock", "^<the0> unlocks <the1> with <a2>.");
            //RMUD.Core.OverrideMessage("convo topic prompt", "Suggested topics: <l0>");
            //RMUD.Core.OverrideMessage("convo cant converse", "You can't converse with that.");
            //RMUD.Core.OverrideMessage("convo greet whom", "Whom did you want to greet?");
            //RMUD.Core.OverrideMessage("convo nobody", "You aren't talking to anybody.");
            //RMUD.Core.OverrideMessage("convo no response", "There doesn't seem to be a response defined for that topic.");
            //RMUD.Core.OverrideMessage("convo no topics", "There is nothing obvious to discuss.");
            //RMUD.Core.OverrideMessage("help topics", "Available help topics");
            //RMUD.Core.OverrideMessage("no help topic", "There is no help available for that topic.");
            //RMUD.Core.OverrideMessage("version", "Build: RMUD Hadad <s0>");
            //RMUD.Core.OverrideMessage("commit", "Commit: <s0>");
            //RMUD.Core.OverrideMessage("no commit", "Commit version not found.");
            //RMUD.Core.OverrideMessage("cons", "Results of consistency check:");
            //RMUD.Core.OverrideMessage("cons no results", "I found nothing wrong.");
        }
    }
}