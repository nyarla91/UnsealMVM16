using Essentials;
using Essentials.Pointers;
using Model.Cards.Spells;
using Model.Combat.GameAreas;
using UnityEngine;
using Zenject;

namespace Model.Cards
{
    public abstract class Card : Transformer
    {
        private const float MovementSpeed = 15;

        private bool _isMouseOver;

        protected PointerTarget PointerTarget { get; private set; }
        protected Spell Spell { get; private set; }

        public Vector3 TargetPosition { get; set; }
        public Quaternion TargetRotation { get; set; }

        public virtual bool ShowPlayableOutline => false;

        [field: Inject] protected GameBoard GameBoard { get; private set; }

        protected void MoveToDeck() => TransformIntoCardInAnotherArea<CardInDeck>();
        protected void MoveToDiscardPile() => TransformIntoCardInAnotherArea<CardInDiscardPile>();

        protected void MoveToHand()
        {
            if (GameBoard.PlayerHand.IsFull)
                return;
            TransformIntoCardInAnotherArea<CardInHand>();
        }

        protected void MoveToBoard()
        {
            if (GameBoard.PlayerBoard.IsFull)
                return;
            TransformIntoCardInAnotherArea<CardOnBoard>();
        }

        private void TransformIntoCardInAnotherArea<T>() where T : Card
        {
            DetachFromPlayArea();
            T card = gameObject.AddComponent<T>();
            GetComponent<ICardPlaceChangedHandler>()?.OnCardPlaceChanged(card);
            card.GameBoard = GameBoard;
            DestroyImmediate(this);
        }

        protected abstract void DetachFromPlayArea();

        protected virtual void Awake()
        {
            PointerTarget = GetComponent<PointerTarget>();
            Spell = GetComponent<Spell>();
        }

        private void FixedUpdate()
        {
            MoveAndRotateTowardsTarget();
        }

        private void MoveAndRotateTowardsTarget()
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, MovementSpeed * Time.fixedDeltaTime);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, TargetRotation, MovementSpeed * Time.fixedDeltaTime);
        }
    }
}