using Model.Cards.Combat;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class OverwatchSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Overwatch",
            "Наблюдение"
        );
        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you play a <cr> gain 1 <ap>",
            "[Пассивно:] Когда вы разыгрываете <cr> получите 1<ap>"
        );
        public override SpellType Type => SpellType.Moon;
        public override bool HasPassive => true;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerHand.OnSpellPlayed += OnSpellPlayed;
        }

        private async void OnSpellPlayed(Spell spell)
        {
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 1, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.PlayerHand.OnSpellPlayed -= OnSpellPlayed;
        }
    }
}