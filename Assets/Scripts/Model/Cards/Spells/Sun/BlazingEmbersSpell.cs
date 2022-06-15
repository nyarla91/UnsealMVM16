using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Sun
{
    public class BlazingEmbersSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Blazing embers",
            "Пылающие угли"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 1<dm> three times",
            "Трижды наносит 1<dm>"
        );
        public override SpellType Type => SpellType.Sun;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            Enemy target = await GetTarget<Enemy>(ChooseEnemyMessage, true);
            for (int i = 0; i < 3; i++)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, target, 1, burst));
            }
        }
    }
}