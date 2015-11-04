using RMUD;
using StandardActionsModule;
using System;
using ConversationModule;

namespace Space.Scenes.Opening
{
    public class Girl : RMUD.NPC
    {
        public override void Initialize()
        {
            Short = "girl";
            this.Article = "the";
            SimpleName("girl");

            Value<MudObject, MudObject, String, String>("printed name").Do((viewer, actor, article) => "the girl");

            Long = "She is perhaps eight, with bangs and a smattering of freckles. She clutches the straps holding her to the flight couch tightly, in anticipation of the upcoming leap. She hasn't said a word the entire flight, that I've heard. Even now she has noticed my attention and is looking away.";

            this.InitializeConversationTopics();

            this.PerformNoTopicsToDiscuss()
                .Do((actor, girl) =>
                {
                    SendMessage(actor, "I can't think of anything else that might draw that child out of her shell.");
                    return SharpRuleEngine.PerformResult.Stop;
                });

            this.PerformGreet()
                .Do((actor, girl) =>
                {
                    SendMessage(actor, "\"Hey.. uh, kid,\" I say. Aukward. She looks at me, but doesn't say anything.");
                    return SharpRuleEngine.PerformResult.Continue;
                });

        }
    }
}
