using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space
{
    public class GameInfo : RMUD.SinglePlayer.GameInfo
    {
        public GameInfo()
        {
            Title = "Space!";
            DatabaseNameSpace = "Space";
            Description = "An adventure in darkest space.";
            Modules = new List<string>(new String[] { "StandardActionsModule.dll", "AdminModule.dll", "ConversationModule.dll", "AliasModule.dll" });
        }
    }
}
