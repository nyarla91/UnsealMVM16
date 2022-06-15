using System;
using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Sun
{
    public class SunflowerSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sunflower",
            "Подсолнух"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Discard:] if its your turn restore 3<hp>",
            "[Сброс:] Если сейчас ваш ход наносит 3<hp>"
        );
        public override SpellType Type => SpellType.Sun;
        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDiscard()
        {
            base.OnDiscard();
            if (!GameBoard.Turn.IsPlayerTurn)
                return;
            
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, GameBoard.Player, 3, false));
        }
    }
}