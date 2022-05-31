using System;
using Essentials;
using Model.Cards.Deckbuilding;
using Model.Cards.Spells.Action;
using Model.Combat.GameAreas;
using Model.Global.Save;
using UnityEngine;
using Zenject;

namespace Model.Deckbulding
{
    public class Libary : CardArea<CardInLibary>
    {
        [SerializeField] private DeckbuildingBoard _deckbuildingBoard;
        [SerializeField] private GameObject _cardPrefab;

        [Inject] private PermanentSave _permanentSave;

        private float _maxZ;
        protected override bool RearrangeAutomatically => false;
        
        private float TargetZ { get; set; }

        protected override void RearrangeCards()
        {
            const float UnitsPerColumn = 2.25f;
            const float UnitsPerRow = 3.25f;
            const int Columns = 5;
            int column = -1;
            int row = 0;
            foreach (var card in Cards)
            {
                column++;
                if (column == Columns)
                {
                    column = 0;
                    row++;
                }
                card.TargetPosition = CardStandart.localPosition + new Vector3(column * UnitsPerColumn, 0, -row * UnitsPerRow);
            }
            _maxZ = row * UnitsPerRow;
        }

        private void Start()
        {
            CreateCard(typeof(HitSpell));
            CreateCard(typeof(DodgeSpell));
            foreach (var cardUnlocked in _permanentSave.Data.CardsUnlocked)
            {
                CreateCard(cardUnlocked);
                CreateCard(cardUnlocked);
                CreateCard(cardUnlocked);
                CreateCard(cardUnlocked);
                CreateCard(cardUnlocked);
                CreateCard(cardUnlocked);
            }
            RearrangeCards();
        }

        private void CreateCard(string spellName)
        {
            Type spellType = Type.GetType(spellName);
            if (spellType == null)
            {
                throw new Exception($"There is no {spellName} spell");
            }
            CreateCard(spellType);
        }

        private void CreateCard(Type spellType)
        {
            GameObject card = Instantiate(_cardPrefab, transform);
            card.AddComponent(spellType);
            CardInLibary cardInLibary = card.AddComponent<CardInLibary>();
            cardInLibary.DeckbuildingBoard = _deckbuildingBoard;
            cardInLibary.Init();
        }

        private void FixedUpdate()
        {
            const float ZMoveSpeed = 30;
            Vector3 localPosition = transform.localPosition;
            transform.localPosition = localPosition.WithZ(Mathf.Lerp(localPosition.z, TargetZ, ZMoveSpeed * Time.fixedDeltaTime));
        }

        private void Update()
        {
            Scroll(Input.mouseScrollDelta);
        }

        private void Scroll(Vector2 scrollDelta)
        {
            const float ScrollSpeed = 100;
            TargetZ -= scrollDelta.y * Time.deltaTime * ScrollSpeed;
            ValidateZ();
        }

        private void ValidateZ()
        {
            TargetZ = Mathf.Clamp(TargetZ,0, _maxZ);
        }
    }
}