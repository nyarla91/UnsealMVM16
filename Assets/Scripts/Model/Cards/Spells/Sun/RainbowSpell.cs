using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class RainbowSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Rainbow",
            "Радуга"
        );

        public override LocalizedString Description => new LocalizedString(
            "[Passive:] Whenever you play or purge passive <cr> restore 1<hp>",
            "[Пассивно:] Когда вы разыгрываете или очищаете пассивную <cr> восстановите 1<hp>"
        );

        public override SpellType Type => SpellType.Sun;
        public override bool HasPassive => true;

        public override void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.PlayerHand.OnSpellPlayed += OnCardPlayPurge;
            GameBoard.PlayerBoard.OnSpellPurged += OnCardPlayPurge;
        }

        private void OnCardPlayPurge(Spell spell)
        {
            if (!spell.HasPassive)
                return;
            
            for (int i = 0; i < GameBoard.PlayerBoard.PassiveModifier; i++)
            {
                GameBoard.EffectQueue.AddEffect(new RestoreHealthEffect(0.1f, GameBoard.Player, 1, Burst), 0);
            }
        }

        public override void OnPurge()
        {
            GameBoard.PlayerHand.OnSpellPlayed -= OnCardPlayPurge;
            GameBoard.PlayerBoard.OnSpellPurged -= OnCardPlayPurge;
            base.OnPurge();
        }
    }
}