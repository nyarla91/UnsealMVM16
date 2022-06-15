using System;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class VenomSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Venom",
            "Яд"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Draw:] Lose 1<hp>.\nThen exile this <cr> and draw a <cr>.",
            "[Взятие:] Потеряйте 1<hp>.\nЗатем изгоните эту <cr> и возьмите <cr>."
        );
        
        public override SpellType Type => SpellType.Nature;

        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDraw()
        {
            base.OnDraw();
            GameBoard.EffectQueue.AddEffect(new WaitEffect(0.5f));
            GameBoard.EffectQueue.AddEffect(new LoseHealthEffect(0.1f, GameBoard.Player, 1), 0);
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace), 1);
            GameBoard.EffectQueue.AddEffect(new DrawTopCardEffect(0.1f, GameBoard.PlayerDeck), 2);
        }
    }
}