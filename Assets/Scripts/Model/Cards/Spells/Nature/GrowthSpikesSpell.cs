using System;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class GrowthSpikesSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Spikes growth",
            "Рост шипов"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Deal 4<dm> to an enemy.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Нанесите 4<dm> противнику.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );
        public override SpellType Type => SpellType.Nature;

        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDraw()
        {
            base.OnDraw();
            GameBoard.EffectQueue.AddEffect(new WaitEffect(0.5f));
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 4, false), 1);
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace), 2);
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck), 3);
        }
    }
}