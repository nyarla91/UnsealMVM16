using System;
using Model.Travel.Map.MoveRequirements;
using TMPro;
using UnityEngine;

namespace View.Travel
{
    public class KeyRequirementPresenter : MonoBehaviour
    {
        [SerializeField] private KeyRequirement _model;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _text.text = _model.Keys.ToString();
        }
    }
}