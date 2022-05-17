using System;
using Model.Combat.Cards.Spells;
using TMPro;
using UnityEngine;

namespace Presenter.Cards
{
    public class CardPresenter : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _icon;
        [SerializeField] private TextMeshPro _name;
        [SerializeField] private TextMeshPro _description;
        [SerializeField] private TextMeshPro _type;

        private Spell _spell;
        
        private void Awake()
        {
            _spell = GetComponent<Spell>();
            UpadteView(_spell);
        }

        private void UpadteView(Spell spell)
        {
            _icon.material = spell.Icon;
            _name.text = spell.Name.Localized;
            _description.text = spell.Description.Localized;
            _type.text = spell.Type.ToString();
        }
    }
}