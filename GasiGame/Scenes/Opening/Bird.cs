using RMUD;
using StandardActionsModule;
using System;
using ConversationModule;

namespace Space.Scenes.Opening
{
    public class Bird : RMUD.NPC
    {
        public override void Initialize()
        {
            Short = "bird";
            this.Article = "the";
            SimpleName("bird");

            Value<MudObject, MudObject, String, String>("printed name").Do((viewer, actor, article) => "the bird");

            Long = "The bird is at least two feet high, and is a riot of colors. Lots of purple and yellow, but plenty of green and orange and red too. It's perched in the center of a transparent geodesic sphere, and appears to be wearing a diaper.";

            this.InitializeConversationTopics();

            this.PerformNoTopicsToDiscuss()
                .Do((actor, girl) =>
                {
                    SendMessage(actor, "Nothing else worth discussing with the avian comes to mind.");
                    return SharpRuleEngine.PerformResult.Stop;
                });

            this.PerformGreet()
                .Do((actor, girl) =>
                {
                    SendMessage(actor,
@"""Hey bird,"" I say.
""Squawk!"" shouts the bird. Then it lets out an impressive hacking cough. ""Excuse me,"" the bird continues. ""What is it, Doctor?""");
                    return SharpRuleEngine.PerformResult.Continue;
                });

            this.DefaultResponse("The bird does not respond.");

            this.Response("ask his name", "The bird responds with a buetiful series of trills and whistles. \"But,\" he adds, \"You can call me Bird, since you do not have the correct anatomy to pronounce my name.\"");

            var responseA = this.Response("ask what kind of bird he is", "\"I am a Macaw,\" the bird says.");

            this.Response("ask how smart he is", "\"Well I'm no astrophysicist,\" the bird says.").Follows(responseA);

            this.Response("ask why he is wearing a diaper", "The bird hops from one foot to the other on his perch. \"Because we're in space? So my shit doesn't get everywhere? What kind of question is that?\"");

        }
    }
}
