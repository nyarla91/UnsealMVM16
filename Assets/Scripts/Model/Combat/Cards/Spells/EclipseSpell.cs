using Model.Combat.Actions;

namespace Model.Combat.Cards.Spells
{
    public class EclipseSpell : Spell
    {
        public override void OnPlay()
        {
            GameBoard.PlayerHand.OnSpellPlayed += OnSpellPlayed;
        }

        private void OnSpellPlayed(Spell spell)
        {
            GameBoard.EffectQueue.AddEffect(new PurgeCardEffect(0.5f, GetComponent<CardOnBoard>()), true);
            GameBoard.PlayerHand.OnSpellPlayed -= OnSpellPlayed;
        }
    }
}