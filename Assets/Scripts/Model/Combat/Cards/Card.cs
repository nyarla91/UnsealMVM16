using Model.Combat.Cards.Spells;
using Model.Combat.GameAreas;
using NyarlaEssentials;
using NyarlaEssentials.Pointers;
using UnityEngine;
using Zenject;

namespace Model.Combat.Cards
{
    public abstract class Card : Transformer
    {
        private const float MovementSpeed = 15;

        protected PointerTarget PointerTarget { get; private set; }
        protected Spell Spell { get; private set; }


        public Vector3 TargetPosition { get; set; }
        public Quaternion TargetRotation { get; set; }

        [field: Inject]
        public GameBoard GameBoard { get; set; }

        protected virtual void MoveToDeck() => TransformIntoCardInAnotherArea<CardInDeck>();

        protected virtual void MoveToDiscardPile() => TransformIntoCardInAnotherArea<CardInDiscardPile>();

        protected virtual void MoveToHand()
        {
            if (GameBoard.PlayerHand.IsFull)
                return;
            TransformIntoCardInAnotherArea<CardInHand>();
        }

        protected virtual void MoveToBoard()
        {
            if (GameBoard.PlayerBoard.IsFull)
                return;
            TransformIntoCardInAnotherArea<CardOnBoard>();
        }

        private void TransformIntoCardInAnotherArea<T>() where T : Card
        {
            DetachFromPlayArea();
            gameObject.AddComponent<T>().GameBoard = GameBoard;
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
            /*
            float distance = MovementSpeed * Time.deltaTime;
            float distancePercentPassed = distance / Vector3.Distance(transform.position, TargetPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, distancePercentPassed);
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, distance);
            */
        }
    }
}