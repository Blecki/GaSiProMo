using RMUD;
using StandardActionsModule;
using System;

namespace Space.Scenes.Opening
{
    public class Girl : RMUD.Actor
    {
        public override void Initialize()
        {
            Short = "girl";
            this.Article = "the";
            SimpleName("girl");

            Value<MudObject, MudObject, String, String>("printed name").Do((viewer, actor, article) => "the girl");

            Long = "She is perhaps eight, with bangs and a smattering of freckles. She clutches the straps holding her to the flight couch tightly, in anticipation of the upcoming leap. She hasn't said a word the entire flight, that I've heard. Even now she has noticed my attention and is looking away.";

        }
    }
}
