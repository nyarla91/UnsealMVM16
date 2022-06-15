using System.Threading.Tasks;
using Essentials;
using Model.Cards.Combat;
using Model.Combat.Characters.Enemies;
using Model.Combat.Effects;
using Model.Localization;

namespace Model.Cards.Spells.Nature
{
    public class VolcanoSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Volcano",
            "Вулкан"
        );
        public override LocalizedString Description => new LocalizedString(
            "Exile your hand. Deal 4<dm> for each <cr> randomly split among all enemies.\nExile this <cr>",
            "Изгоните свою руку. Нвносит 4<dm> за каждую <cr> случайно распределяя между противниками.\nИзгоните эту <cr>"
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);

            foreach (CardInHand cardInHand in GameBoard.PlayerHand.Cards)
            {
                for (int i = 0; i < 4 + (burst ? 1 : 0); i++)
                {
                    Enemy target = GameBoard.EnemyPool.ActiveEnemies.PickRandomElement();
                    GameBoard.EffectQueue.AddEffect(new DealDamageEffect(0.05f, target, 1, false));
                }
                GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, cardInHand));
            }
            await GameBoard.EffectQueue.WaitForEffects();
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace));
        }
    }
}