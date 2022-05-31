using System;
using System.Collections.Generic;
using Essentials;
using Model.Cards;
using UnityEngine;

namespace Model.Combat.GameAreas
{
    public abstract class CardArea<T> : Transformer where T : Card
    {

        [SerializeField] private Transform _cardStandart;
        
        public Transform CardStandart => _cardStandart;
        protected List<T> Cards { get; set; } = new List<T>();
        public int Size => Cards.Count;
        public bool IsFull => Size == MaxSize;
        protected virtual bool RearrangeAutomatically => true;
        protected virtual int MaxSize => Int32.MaxValue;
        
        public void AddCard(T cardToAdd)
        {
            Cards.Add(cardToAdd);
            cardToAdd.transform.parent = transform;
            if (RearrangeAutomatically)
                RearrangeCards();
        }

        public void RemoveCard(T cardToRemove)
        {
            Cards.Remove(cardToRemove);
            cardToRemove.transform.parent = null;
            if (RearrangeAutomatically) 
                RearrangeCards();
        }

        protected abstract void RearrangeCards();
    }
}