using System;
using System.Collections.Generic;
using Essentials;
using Model.Cards;
using Presenter.Cards;
using UnityEngine;
using UnityEngine.Serialization;
using View.Cards;
using Zenject;

namespace Model.Combat.GameAreas
{
    public abstract class CardArea<TCard> : Transformer where TCard : Card
    {
        [SerializeField] private GameObject _cardPrefab;
        [SerializeField] private Transform _cardStandart;
        [FormerlySerializedAs("abilitiyTooltip")] [SerializeField] private AbilitiyTooltip _abilitiyTooltip;

        [Inject] private Pause _pause;

        protected Transform CardStandart => _cardStandart;
        public List<TCard> Cards { get; protected set; } = new List<TCard>();
        public int Size => Cards.Count;
        public bool IsFull => Size == MaxSize;
        protected virtual bool RearrangeAutomatically => true;
        protected virtual int MaxSize => Int32.MaxValue;
        
        public void AddCard(TCard cardToAdd)
        {
            Cards.Add(cardToAdd);
            cardToAdd.transform.parent = transform;
            if (RearrangeAutomatically)
                RearrangeCards();
        }

        public void RemoveCard(TCard cardToRemove)
        {
            Cards.Remove(cardToRemove);
            cardToRemove.transform.parent = null;
            if (RearrangeAutomatically) 
                RearrangeCards();
        }

        public TCard CreateCard(string spellName, Vector3 startingPosition)
        {
            Type spellType = Type.GetType(spellName);
            if (spellType == null)
            {
                throw new Exception($"There is no {spellName} spell");
            }
            return CreateCard(spellType, startingPosition);
        }

        public TCard CreateCard(Type spellType, Vector3 startingPosition)
        {
            GameObject card = Instantiate(_cardPrefab, startingPosition, Quaternion.identity, transform);
            card.AddComponent(spellType);
            TCard cardInPlace = card.AddComponent<TCard>();
            PassBoard(cardInPlace);
            cardInPlace.Pause = _pause;
            cardInPlace.GetComponent<CardPresenter>().Tooltip = _abilitiyTooltip;
            cardInPlace.Init();
            return cardInPlace;
        }

        protected abstract void PassBoard(TCard cardInPlace);

        protected abstract void RearrangeCards();
    }
}