using Model.Combat.Effects;

namespace Model.Combat.Characters
{
    public class Player : Character
    {
        private void Awake()
        {
            GameBoard.Turn.OnPlayerTurnStart += OnPlayerTurnStart;
        }

        private async void OnPlayerTurnStart()
        {
            EffectQueue queue = GameBoard.EffectQueue;
            queue.Delay(0.5f);
            queue.AddEffect(new ClearArmorEffect( 0.4f, this));
            queue.AddEffect(new TriggerPereodicDamageEffect(0.2f, this));
        }

        private void OnDestroy()
        {
            GameBoard.Turn.OnPlayerTurnStart -= OnPlayerTurnStart;
        }
    }
}