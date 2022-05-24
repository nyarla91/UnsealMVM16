﻿using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Action
{
    public class ManeuverSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Maneuver",
            "Манёвр"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 2<ap>\n[Purge:] Gain 2<ap>",
            "Получите 2<ap>\n[Очищение:] Получите 2<ap>"
        );
        public override SpellType Type => SpellType.None;
        
        public override void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, growth));
        }

        public override void OnPurge()
        {
            GameBoard.EffectQueue.InsertEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, Growth), 0);
            base.OnPurge();
        }
    }
}