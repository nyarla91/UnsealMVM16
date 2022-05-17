using System;
using Model.Combat.TargetChoose;
using UnityEngine;

namespace Presenter.Combat
{
    public class TargetToChoosePresenter : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _highlighter;
        [SerializeField] private Material _invisibleMaterial;
        [SerializeField] private Material _availableMaterial;
        [SerializeField] private Material _chosenMaterial;

        private TargetToChoose _targetToChoose;
        private bool _chosen;

        private void DisableHighliht()
        {
            _chosen = false;
            _highlighter.material = _invisibleMaterial;
        }

        private void HighlightAvaillable()
        {
            _highlighter.material = _availableMaterial;
        }

        private void SwitchChosenHighlight()
        {
            _chosen = !_chosen;
            _highlighter.material = _chosen ? _chosenMaterial : _availableMaterial;
        }

        private void Awake()
        {
            _targetToChoose = GetComponent<TargetToChoose>();
            _targetToChoose.OnStartChoosing += HighlightAvaillable;
            _targetToChoose.OnEndChoosing += DisableHighliht;
            _targetToChoose.OnChooseSwitch += SwitchChosenHighlight;
        }
    }
}