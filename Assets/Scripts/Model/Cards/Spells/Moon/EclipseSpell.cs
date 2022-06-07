using Model.Combat.Characters;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class EclipseSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Eclipse",
            "Закат"
        );
        public override LocalizedString Description => new LocalizedString(
            "Passive: Every time you play a spell add 1 charge to this card.\nPurge: Deal 1 damage to an enemy for each charge",
            "Пассиво: Каждый раз, когда вы разыграете заклинание добаляейте 1 заряд на эту карту\nОчищение: Наносит выбранному противнику 1 ед. урона за каждый заряд"
        );
        public override SpellType Type => SpellType.Sun;
        
        private Character _target;
        
        public override async void OnPlay(bool growth)
        {
            base.OnPlay(growth);
            GameBoard.PlayerHand.OnSpellPlayed += OnSpellPlayed;
        }

        private void OnSpellPlayed(Spell spell)
        {
            Charges++;
        }

        public override async void OnPurge()
        {
            GameBoard.PlayerHand.OnSpellPlayed -= OnSpellPlayed;
            int damage = Charges;
            Character target = await GetTarget<Character>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage, Growth), 0);
            base.OnPurge();
        }
    }
}