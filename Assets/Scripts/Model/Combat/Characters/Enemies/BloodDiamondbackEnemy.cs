using Model.Combat.Effects;

namespace Model.Combat.Characters.Enemies
{
    public class BloodDiamondbackEnemy : Enemy
    {
        private void Awake()
        {
            OnLoseHealth += GetArmor;
        }

        private void GetArmor(int healthLost)
        {
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, this, healthLost, false));
        }
    }
}