using System;
using System.Threading.Tasks;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class GrowthRootsSpell : GrowthSpell
    {
        public override LocalizedString Name => new LocalizedString(
            "Roots growth",
            "Рост корней"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Gain 3<ap>.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Получите 3<ap>.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );

        protected override async Task PerformGrowthSpecificEffect(int index)
        {
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 3, false), index);
        }
    }
}