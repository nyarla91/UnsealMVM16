using Model.Cards.Spells;

namespace Model.Combat.Characters.Enemies
{
    public class MoonTickerEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
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