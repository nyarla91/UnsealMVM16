using System.Threading.Tasks;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class TornadoSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Tornado",
            "Торнадо"
        );
        public override LocalizedString Description => new LocalizedString(
            "Deal 4<dm> to all enemies.\nExile this <cr>",
            "Нанесите 4<dm> всем противникам\nИзгоните эту <cr>"
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            foreach (var target in GameBoard.EnemyPool.ActiveEnemies)
            {
                GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.1f, target, 4, burst));
            }
            await GameBoard.EffectQueue.WaitForEffects();
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace));
        }
    }
}