using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class TrickSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Trick",
            "Уловка"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm> to an enemy.\n[Purge:] Gain 2<ap>",
            "Наносит 2<dm> протианику.\n[Очищение:] Получите 2<ap>"
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 2, burst));
        }

        public override void OnPurge()
        {
            base.OnPurge();
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 2, Burst));
        }
    }
}