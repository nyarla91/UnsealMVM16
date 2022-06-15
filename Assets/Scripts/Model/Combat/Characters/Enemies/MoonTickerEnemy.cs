using Model.Cards.Spells;

namespace Model.Combat.Characters.Enemies
{
    public class MoonTickerEnemy : Enemy
    {
        private void Awake()
        {
            GameBoard.PlayerHand.OnSpellPlayed += OnSpellPlayed;            
        }

        private void OnSpellPlayed(Spell spell)
        {
            DealDamage(1);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameBoard.PlayerHand.OnSpellPlayed -= OnSpellPlayed;       
        }
    }
}