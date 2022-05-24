using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Essentials;
using Model.Combat.GameAreas;
using Model.Localization;
using UnityEngine;
using Zenject;

namespace Model.Cards.Spells
{
    public abstract class Spell : Transformer
    {
        private int _charges;
        private Material _icon;
        public abstract LocalizedString Name { get; }
        public abstract LocalizedString Description { get; }
        public abstract SpellType Type { get; }
        public virtual Func<bool> Requirements => () => true;
        public Material Icon => _icon ??= Resources.Load<Material>("SpellIcons/" + Name.Eng);

        [field: Inject]
        protected GameBoard GameBoard { get; set; }

        protected bool Growth { get; private set; }

        public int Charges
        {
            get => _charges;
            set
            {
                value = Mathf.Max(value, 0);
                if (_charges == value)
                    return;
                
                OnChargesChanged?.Invoke(value);
                _charges = value;
            }
        }

        public event Action<int> OnChargesChanged;

        public virtual void OnPlay(bool growth)
        {
            Growth = growth;
        }

        public virtual void OnPurge()
        {
            Charges = 0;
        }
        public virtual void OnDraw(){}

        public virtual void OnDiscard()
        {
            Charges = 0;
        }

        private void Awake()
        {
            GetComponent<ISpellAddedHandler>()?.OnSpellAdded(this);
        }

        protected async Task<List<T>> GetTargets<T>(int ammount, bool exactNumber) where T : MonoBehaviour
        {
            return await GameBoard.TargetChooser.StartTargetsChoose<T>(transform, ammount, exactNumber);
        }

        protected async Task<T> GetTarget<T>(bool compulsory)where T : MonoBehaviour
        {
            List<T> targets = await GetTargets<T>(1, compulsory);
            return targets.Count > 0 ? targets[0] : null;
        }

        private void OnDestroy()
        {
            GetComponent<ISpellRemovedHandler>()?.OnSpellRemoved();
        }
    }

    public enum SpellType
    {
        Nature,
        Sun,
        Moon,
        Blood,
        Action,
        None,
    }
}