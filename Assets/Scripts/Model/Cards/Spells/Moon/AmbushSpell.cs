using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class AmbushSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Ambush",
            "Засада"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal <dm> equal to your <ap>",
            "Наносит <dm> равный вашей <ap>"
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, damage: GameBoard.Player.Armor, burst));
        }
    }
}