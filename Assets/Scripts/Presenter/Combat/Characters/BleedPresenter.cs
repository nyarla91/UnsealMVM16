using System;
using System.Collections.Generic;
using Essentials;
using Model.Combat.Characters;
using TMPro;
using UnityEngine;
using View;
using View.Combat.Characters;

namespace Presenter.Combat.Characters
{
    public class BleedPresenter : ComponentInstantiator
    {
        [SerializeField] private Token _tokenPrefab;
        [SerializeField] private Character _character;

        private List<Token> _bleedTokens = new List<Token>();

        private void Awake()
        {
            _character.OnBleedAdded += CreateBleedToken;
            _character.OnBleedValueChanged += UpdateBleedToken;
            _character.OnBleedRemoved += DestroyBleedToken;
        }

        private void CreateBleedToken(int value)
        {
            InstantiateForComponent(out Token token, _tokenPrefab, transform.position);
            _bleedTokens.Add(token);
            token.Value = value;
            token.Descend();
            RearrangeTokenPositions();
        }

        private void UpdateBleedToken(int index, int value)
        {
            _bleedTokens[index].Value = value;
            _bleedTokens[index].Flip();
        }

        private void DestroyBleedToken(int index)
        {
            _bleedTokens[index].Ascend(true);
            _bleedTokens.RemoveAt(index);
            RearrangeTokenPositions();
        }

        private void RearrangeTokenPositions()
        {
            const float Spacing = 0.6f;
            const int TokensInRow = 6;
            const float RowsSpacing = 1;
            
            for (int i = _bleedTokens.Count - 1; i >= 0; i--)
            {
                float x = (i + 1) % TokensInRow * Spacing - Spacing;
                float z = ((int) ((i + 1) / TokensInRow)) * -RowsSpacing;
                Vector3 position = transform.position + new Vector3(-1 + x, 0, -1 + z);
                _bleedTokens[i].TargetPosition = position;
            }
        }
    }
}