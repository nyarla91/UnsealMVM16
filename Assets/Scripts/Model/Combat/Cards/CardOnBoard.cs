using Model.Combat.Actions;
using PointerType = NyarlaEssentials.Pointers.PointerType;
using Vector3 = UnityEngine.Vector3;

namespace Model.Combat.Cards
{
    public sealed class CardOnBoard : Card
    {
        [DontCallFromSpells]
        public void Purge()
        {
            Spell.OnPurge();
            MoveToDiscardPile();
        }
        
        protected override void DetachFromPlayArea()
        {
            GameBoard.PlayerBoard.RemoveCard(this);
        }

        private void Start()
        {
            GameBoard.PlayerBoard.AddCard(this);
            PointerTarget.OnClick += OnClick;
        }

        private void OnClick(PointerType button, Vector3 contactpoint)
        {
            
        }

        private void OnDestroy()
        {
            PointerTarget.OnClick -= OnClick;
        }
    }
}