namespace Model.Combat.Characters.Enemies
{
    public class SunRamEnemy : Enemy
    {
        protected override void BeforeActivation()
        {
            base.BeforeActivation();
            DamageBonus = 0;
        }

        protected override void AfterActivation()
        {
            base.AfterActivation();
            DamageBonus = Armor;
        }
    }
}