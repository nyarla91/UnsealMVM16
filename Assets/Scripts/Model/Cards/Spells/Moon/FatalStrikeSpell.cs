using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class FatalStrikeSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Fatal strike",
            "Роковой удар"
        );
        public override LocalizedString Description => new LocalizedString(
            "Spend all your <ap> and deal twice as much <dm> to an enemy",
            "Потратьте всю вашу <ap> и нанесите вдаое больше урона противнику."
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            int armor = GameBoard.Player.Armor;
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new ClearArmorEffect(0.1f, GameBoard.Player));
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, armor, Burst));
        }
    }
}