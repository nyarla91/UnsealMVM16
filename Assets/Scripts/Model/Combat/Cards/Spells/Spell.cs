using Model.Combat.GameAreas;
using Model.Combat.TargetChoose;
using Model.Localization;
using UnityEngine;
using Zenject;

namespace Model.Combat.Cards.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] private Material _icon;
        [SerializeField] private LocalizedString _name;
        [SerializeField] private LocalizedString _description;
        [SerializeField] private SpellType _type;

        protected TargetToChoose TargetToChoose { get; private set; }
        
        [field: Inject]
        protected GameBoard GameBoard { get; set; }
        
        public Material Icon => _icon;
        public LocalizedString Name => _name;
        public LocalizedString Description => _description;
        public SpellType Type => _type;

        public abstract void OnPlay();
        public virtual void OnPurge(){}
        public virtual void OnDraw(){}
        public virtual void OnDiscard(){}

        private void Start()
        {
            TargetToChoose = GetComponent<TargetToChoose>();
        }
    }

    public enum SpellType
    {
        Nature,
        Sun,
        Moon,
        Blood,
    }
}