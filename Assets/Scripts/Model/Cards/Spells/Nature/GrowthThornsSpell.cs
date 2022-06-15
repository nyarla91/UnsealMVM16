using System;
using System.Threading.Tasks;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class GrowthThornsSpell : GrowthSpell
    {
        public override LocalizedString Name => new LocalizedString(
            "Thorns growth",
            "Рост шипов"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Deal 3<dm> to an enemy.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Нанесите 3<dm> противнику.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );

        protected override async Task PerformGrowthSpecificEffect(int index)
        {
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 3, false), index);
        }
    }
}