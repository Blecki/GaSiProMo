using RMUD;
using StandardActionsModule;
using System;
using ConversationModule;

namespace Space.Scenes.Opening
{
    public class Daniel : RMUD.NPC
    {
        public override void Initialize()
        {
            SimpleName("Daniel", "dan");

            Value<MudObject, MudObject, String, String>("printed name").Do((viewer, actor, article) => "Daniel");

            bool firstLook = true;

            this.PerformDescribe().FirstTimeOnly.Do((viewer, actor) =>
            {
                if (firstLook)
                {
                    firstLook = false;
                    SendMessage(viewer, "Daniel is my son. He looks more like his mother than me. This is his first interstellar trip, too. I thought to calm him as we prepared for the jump.");
                    Game.StartConversation(viewer as Actor, actor);
                }
                else
                    SendMessage(viewer, "Daniel is thirteen now, which means he has seen everything there is to see.");
                return SharpRuleEngine.PerformResult.Continue;
            });

            this.Response("ask if he is excited", "\"No,\" Daniel says. I suppose it's a stage. I'm a xenobiologist, not a child psychologist. He's been like this since his mother passed. Maybe the verdant world will be good for us.");

            this.Response("ask if he is worried about the jump", "Daniel shruggs. \"We already did three.\"");

        }
    }
}
