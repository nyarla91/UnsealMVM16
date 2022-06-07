using System;
using DG.Tweening;
using Essentials;
using Essentials.Pointers;
using Presenter.Combat;
using TMPro;
using UnityEngine;

namespace View.Combat
{
    public class CombatButtonView : ButtonView
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private TMP_Text _text;

        public void UpdateState(CombatButtonState state)
        {
            _meshRenderer.material = state.Material;
            _text.text = state.Text.Localized;
        }
    }
}