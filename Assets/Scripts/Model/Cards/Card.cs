using Essentials;
using Essentials.Pointers;
using Model.Cards.Spells;
using Model.Combat.Effects;
using UnityEngine;

namespace Model.Cards
{
    public abstract class Card : Transformer
    {
        private const float MovementSpeed = 15;

        private bool _isMouseOver;

        protected PointerTarget PointerTarget { get; private set; }
        public Spell Spell { get; private set; }

        public Vector3 TargetPosition { get; set; }
        public Quaternion TargetRotation { get; set; }

        public virtual bool ShowPlayableOutline => false;
        protected virtual bool LocalPosition => false;


        protected void TransformIntoCardInAnotherArea<T>() where T : Card
        {
            DetachFromPlayArea();
            T card = gameObject.AddComponent<T>();
            PassBoard(card);
            card.Init();
            DestroyImmediate(this);
        }

        [DontCallFromSpells]
        public void Exile()
        {
            DetachFromPlayArea();
            Destroy(gameObject);
        }

        protected abstract void PassBoard(Card card);

        protected abstract void DetachFromPlayArea();
        public abstract void Init();

        protected virtual void Awake()
        {
            TargetPosition = LocalPosition ? transform.localPosition : transform.position;
            PointerTarget = GetComponent<PointerTarget>();
            Spell = GetComponent<Spell>();
            foreach (ICardPlaceChangedHandler handler in GetComponents<ICardPlaceChangedHandler>())
            {
                handler?.OnCardPlaceChanged(this);
            }
        }

        private void FixedUpdate()
        {
            MoveAndRotateTowardsTarget();
        }

        private void MoveAndRotateTowardsTarget()
        {
            if (LocalPosition)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, TargetPosition, MovementSpeed * Time.fixedDeltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, TargetPosition, MovementSpeed * Time.fixedDeltaTime);
            }
            
            transform.rotation =
                Quaternion.Lerp(transform.rotation, TargetRotation, MovementSpeed * Time.fixedDeltaTime);
        }
    }
}