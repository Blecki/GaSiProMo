using RMUD;
using StandardActionsModule;
using System;

namespace Space.Scenes.Opening
{
    public class Sal : RMUD.Actor
    {
        public override void Initialize()
        {
            Short = "Sal";
            this.Article = "";
            SimpleName("sal");

            Value<MudObject, MudObject, String, String>("printed name").Do((viewer, actor, article) => "Sal");

        }
    }
}
