using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Combat.GameAreas;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Sun
{
    public class SunsetSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sunset",
            "Закат"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap> for each your passive card on the board",
            "Полусите 2<ap> за каждую вашу пассивную карту на поле."
        );
        public override SpellType Type => SpellType.Sun;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (CardOnBoard cardOnBoard in GameBoard.PlayerBoard.Cards)
            {
                if (cardOnBoard.Spell.HasPassive)
                    GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.05f, GameBoard.Player, 2, burst));
            }
        }
    }
}