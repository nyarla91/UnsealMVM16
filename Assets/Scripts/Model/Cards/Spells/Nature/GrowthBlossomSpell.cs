using System;
using System.Threading.Tasks;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class GrowthBlossomSpell : GrowthSpell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blossom growth",
            "Рост цветка"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Restore 2<hp>.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Восстановите 2<hp>.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );

        protected override async Task PerformGrowthSpecificEffect(int index)
        {
            GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 2, false), index);
        }
    }
}