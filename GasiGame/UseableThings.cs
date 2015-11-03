using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMUD;
using SharpRuleEngine;

namespace Space
{
    public class UseableThings : CommandFactory
    {
        public override void Create(CommandParser Parser)
        {
            Parser.AddCommand(
                Or(
                    Sequence(
                        KeyWord("USE"),
                        BestScore("SUBJECT",
                            MustMatch("@not here",
                            Object("SUBJECT", InScope, (actor, thing) =>
                            {
                                // Prefer objects we can actually use.
                                var canUse = Core.GlobalRules.ConsiderCheckRuleSilently("can use?", actor, thing);
                                if (canUse == SharpRuleEngine.CheckResult.Allow)
                                    return MatchPreference.Likely;
                                return MatchPreference.Unlikely;
                            })))),
                    // We would also like to match when the player types the name of a thing that is
                    // useable. Here we just check the 'useable?' property, so that things where 
                    // 'can use?' fails will still be matched.
                    Generic((m, c) =>
                    {
                        return new List<RMUD.PossibleMatch>(
                            Object("SUBJECT", InScope)
                                .Match(m, c)
                                .Where(
                                _ => (_["SUBJECT"] as MudObject).GetBooleanProperty("useable?")));
                    }, "[A USEABLE ITEM]")))
                .ID("USE")
                .Manual("Use a useable thing. You can also just enter the name of the thing.")
                .Check("can use?", "ACTOR", "SUBJECT")
                .BeforeActing()
                .Perform("used", "ACTOR", "SUBJECT")
                .AfterActing();
        }

        public static void AtStartup(RMUD.RuleEngine GlobalRules)
        {
            Core.StandardMessage("not useable", "I don't know what I would do with that.");

            GlobalRules.DeclareCheckRuleBook<MudObject, MudObject>("can use?", "[Actor, Item] : Can the actor use the item?", "actor", "item");

            GlobalRules.DeclarePerformRuleBook<MudObject, MudObject>("used", "[Actor, Item] : Handle the actor using the item.", "actor", "item");

            GlobalRules.Check<MudObject, MudObject>("can use?")
                .When((actor, item) => !item.GetBooleanProperty("useable?"))
                .Do((a, b) =>
                {
                    MudObject.SendMessage(a, "@not useable");
                    return SharpRuleEngine.CheckResult.Disallow;
                })
                .Name("Can't use the unuseable rule.");

            GlobalRules.Check<MudObject, MudObject>("can use?")
                .Do((a, b) => SharpRuleEngine.CheckResult.Allow)
                .Name("Default go ahead and use it rule.");

            GlobalRules.Perform<MudObject, MudObject>("used").Do((actor, target) =>
            {
                MudObject.SendMessage(actor, "It doesn't do anything.");
                return SharpRuleEngine.PerformResult.Continue;
            }).Name("Default report using rule.");

            GlobalRules.Check<MudObject, MudObject>("can use?").First.Do((actor, item) => MudObject.CheckIsVisibleTo(actor, item)).Name("Item must be visible rule.");
        }
    }

    public static class UseExtensions
    {
        public static SharpRuleEngine.RuleBuilder<MudObject, MudObject, PerformResult> PerformUsed(this MudObject Object)
        {
            return Object.Perform<MudObject, MudObject>("used").ThisOnly(1);
        }

        public static RuleBuilder<MudObject, MudObject, CheckResult> CheckCanUse(this MudObject Object)
        {
            return Object.Check<MudObject, MudObject>("can use?").ThisOnly(1);
        }

        public static void MakeUseable(this MudObject Object)
        {
            Object.SetProperty("useable?", true);
        }
    }
}