using Model.Cards.Combat;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class SpikedVineSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Spiked vine",
            "Колючая лоза"
        );
        public override LocalizedString Description => new LocalizedString(
            "Add 2 Thorns Growth to your discard pile",
            "добавьте 2 Роста Шипов в ваш сброс."
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            for (int i = 0; i < 2; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddSpellToAreaEffect<CardInDiscardPile>(0.1f,
                    GameBoard.PlayerDiscardPile, typeof(GrowthThornsSpell)));
            }
        }
    }
}