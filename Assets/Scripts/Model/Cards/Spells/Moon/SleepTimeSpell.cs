using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Moon
{
    public class SleepTimeSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Sleep time",
            "Время спать"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain <ap> equal to the attack of target enemy.",
            "Получите <ap> равный атаке выбранного противника."
        );
        public override SpellType Type => SpellType.Moon;
        
        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            if (target == null)
                return;
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, target.AttackPerTurn, Burst));
        }
    }
}