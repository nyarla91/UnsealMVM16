using Model.Cards.Combat;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Moon
{
    public class LayLowSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Lay low",
            "Затаиться"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Purge:] if its your then gain 6<ap>",
            "[Очищение:] Если сейчас ваш ход получите 6<ap>"
        );
        public override SpellType Type => SpellType.Moon;
        public override void OnPurge()
        {
            base.OnPurge();
            if (!GameBoard.Turn.IsPlayerTurn)
                return;
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.2f, GameBoard.Player, 6, Burst));
        }
    }
}