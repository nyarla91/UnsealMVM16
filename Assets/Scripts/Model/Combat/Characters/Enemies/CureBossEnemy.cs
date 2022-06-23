using Model.Cards.Combat;
using Model.Combat.Effects;

namespace Model.Combat.Characters.Enemies
{
    public class CureBossEnemy : Enemy
    {
        protected override void Start()
        {
            base.Start();
            GameBoard.Turn.OnFormNotChanged += SpikeEffect;
        }

        private void SpikeEffect()
        {
            foreach (CardInHand card in GameBoard.PlayerHand.Cards)
            {
                GameBoard.EffectQueue.AddEffect(new DiscardACardEffect(0.1f, card), 0);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameBoard.Turn.OnFormNotChanged -= SpikeEffect;
        }
    }
}