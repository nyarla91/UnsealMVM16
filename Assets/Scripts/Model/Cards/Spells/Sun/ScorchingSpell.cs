using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class ScorchingSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Scorching",
            "Опаление"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 2<dm>. Gain 1<br>",
            "Наносит 2<dm>. Получите 1<br>"
        );
        public override SpellType Type => SpellType.Sun;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy enemy = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (enemy == null)
                return;
            GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, enemy, 2, burst));
            GameBoard.EffectQueue.AddEffect(new AddBurstEffect(0.1f, GameBoard.PlayerBoard, 1));
        }
    }
}