using System;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class GrowthRootsSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Roots growth",
            "Рост корней"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Gain 3<ap>.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Получите 3<ap>.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );
        public override SpellType Type => SpellType.Nature;

        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDraw()
        {
            base.OnDraw();
            GameBoard.EffectQueue.AddEffect(new WaitEffect(0.5f));
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, target, 3, false), 1);
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace), 2);
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck), 3);
        }
    }
}