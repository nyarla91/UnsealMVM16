using System;
using Model.Combat.Characters;
using TMPro;
using UnityEngine;

namespace Presenter.Combat.Characters
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _healthText;
        [SerializeField] private Character _character;

        private void Awake()
        {
            _character.OnHealthChanged += UpdateHealth;
        }

        private void UpdateHealth(int newHealth)
        {
            _healthText.text = newHealth.ToString();
        }
    }
}