using System.Threading.Tasks;
using Model.Combat.Effects;
using Model.Localization;
using UnityEngine;

namespace Model.Cards.Spells.Nature
{
    public class AmberShellSpell : Spell
    {
        public override LocalizedString Name => new LocalizedString(
            "Amber shell",
            "Янтарная оболочка"
        );
        public override LocalizedString Description => new LocalizedString(
            "Gain 8<ap>\nExile this <cr>",
            "Получите 8<ap>\nИзгоните эту <cr>"
        );
        public override SpellType Type => SpellType.Nature;

        public override async void OnPlay(bool burst)
        {
            base.OnPlay(burst);
            GameBoard.EffectQueue.AddEffect(new AddArmorEffect(0.1f, GameBoard.Player, 8, burst));
            await GameBoard.EffectQueue.WaitForEffects();
            GameBoard.EffectQueue.AddEffect(new ExileCardEffect(0.1f, CardPlace));
        }
    }
}