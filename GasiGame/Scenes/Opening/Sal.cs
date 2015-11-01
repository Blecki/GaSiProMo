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

            var firstLook = true;
            this.PerformDescribe().Do((viewer, actor) =>
            {
                if (firstLook)
                {
                    firstLook = false;
                    SendMessage(viewer, "Sal has that long time spacer look. There is hardly anything to her, she is all long limbs, held together as much by her jumpsuit as by muscle and sinew. She is not unatractive, but there is something I find unappealing about her. I can't put my finger on exactly what.");
                    Game.StartConversation(viewer as Actor, actor);
                }
                else
                    SendMessage(viewer, "Sal is scowling about something. At least her safety briefing is over.");

                return SharpRuleEngine.PerformResult.Continue;
            });
        }
    }
}
