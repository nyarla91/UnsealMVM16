using System.Collections.Generic;
using Essentials;
using Model.Combat.Characters;
using UnityEngine;
using View.Combat.Characters;

namespace Presenter.Combat.Characters
{
    public class BleedPresenter : ComponentInstantiator
    {
        [SerializeField] private Token _tokenPrefab;
        [SerializeField] private Transform _origin;
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
            InstantiateForComponent(out Token token, _tokenPrefab, _origin);
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
            const float RowsSpacing = 0.6f;
            
            for (int i = _bleedTokens.Count - 1; i >= 0; i--)
            {
                int position = i + 1;
                float x = position % TokensInRow * Spacing;
                float z = (position / TokensInRow) * -RowsSpacing;
                _bleedTokens[i].TargetLocalPosition = new Vector3(x, 0, z);
            }
        }
    }
}