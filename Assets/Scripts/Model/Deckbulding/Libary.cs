using System;
using System.Collections.Generic;
using System.Linq;
using Essentials;
using Model.Cards.Deckbuilding;
using Model.Cards.Spells;
using Model.Global.Save;
using UnityEngine;
using Zenject;

namespace Model.Deckbulding
{
    public class Libary : DeckbuildingCardArea<CardInLibary>
    {
        [Inject] private PermanentSave _permanentSave;
        [Inject] private ManualSave _manualSave;

        private List<CardInLibary> _cardsToShow;
        private float _maxZ;
        
        protected override bool RearrangeAutomatically => true;
        
        private float TargetZ { get; set; }

        public List<Func<Spell, bool>> FilterCriterias { get; private set; } = new List<Func<Spell, bool>>();

        public void FilterAndRearrange() => RearrangeCards();

        public void Filter()
        {
            Func<Spell, bool>[] criterias =
                FilterCriterias.Count == 0 ? new Func<Spell, bool>[]{spell => true} : FilterCriterias.ToArray();
            
            _cardsToShow = new List<CardInLibary>();

            foreach (CardInLibary card in Cards)
            {
                for (var i = 0; i < criterias.Length; i++)
                {
                    if (criterias[i].Invoke(card.Spell))
                    {
                        card.gameObject.SetActive(true);
                        _cardsToShow.Add(card);
                        break;
                    }

                    if (i == criterias.Length - 1)
                        card.gameObject.SetActive(false);
                }
            }
        }
        
        protected override void RearrangeCards()
        {
            Filter();
            const float UnitsPerColumn = 2.25f;
            const float UnitsPerRow = 3.25f;
            const int Columns = 5;
            int column = -1;
            int row = 0;
            foreach (var card in _cardsToShow)
            {
                column++;
                if (column == Columns)
                {
                    column = 0;
                    row++;
                }
                card.TargetPosition = CardStandart.localPosition + new Vector3(column * UnitsPerColumn, 0, -row * UnitsPerRow);
                card.TargetRotation = CardStandart.rotation;
            }
            _maxZ = row * UnitsPerRow;
        }

        private void Start()
        {
            foreach (var cardUnlocked in _permanentSave.Data.CardsUnlocked)
            {
                CardInLibary createdCard = CreateCard(cardUnlocked, transform.position);
                if (!createdCard.Spell.InfiniteInDeck && _manualSave.Data.Deck.Contains(cardUnlocked))
                    createdCard.Exile();
            }
        }

        private void FixedUpdate()
        {
            const float ZMoveSpeed = 15;
            Vector3 localPosition = transform.localPosition;
            transform.localPosition = localPosition.WithZ(Mathf.Lerp(localPosition.z, TargetZ, ZMoveSpeed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            Scroll(Input.mouseScrollDelta);
        }

        private void Scroll(Vector2 scrollDelta)
        {
            const float ScrollSpeed = 200;
            TargetZ -= scrollDelta.y * Time.deltaTime * ScrollSpeed;
            ValidateZ();
        }

        private void ValidateZ()
        {
            TargetZ = Mathf.Clamp(TargetZ,0, _maxZ);
        }
    }
}