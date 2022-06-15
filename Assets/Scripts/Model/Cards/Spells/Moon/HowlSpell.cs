using System;
using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Moon
{
    public class HowlSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Howl",
            "Вой"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Discard:] Purge a <cr>",
            "[Сброс:] Очистите карту <cr>"
        );
        public override SpellType Type => SpellType.Moon;

        public override Func<bool> PlayRequirements => () => false;

        public override async void OnDiscard()
        {
            base.OnDiscard();
            CardOnBoard target = await GetTarget<CardOnBoard>(ChooseCardToPurgeMessage, true);
            if (target == null)
                return;
            GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.1f, target), 0);
        }
    }
}