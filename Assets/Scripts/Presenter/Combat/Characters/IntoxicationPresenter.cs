using System.Collections.Generic;
using Essentials;
using Model.Combat.Characters;
using UnityEngine;
using View.Combat.Characters;

namespace Presenter.Combat.Characters
{
    public class IntoxicationPresenter : ComponentInstantiator
    {
        [SerializeField] private Token _tokenPrefab;
        [SerializeField] private Transform _origin;
        [SerializeField] private Player _player;

        private List<Token> _tokens = new List<Token>();
        
        private void Awake()
        {
            _player.OnIntoxicationAdded += AddTokens;
            _player.OnIntoxicationCured += RemoveTokens ;
        }

        private void RemoveTokens(int tokens)
        {
            for (int i = 0; i < tokens; i++)
            {
                _tokens[^1].Ascend(true);
                _tokens.RemoveAt(_tokens.Count - 1);
            }
            RearrangeTokenPositions();
        }

        private void AddTokens(int tokens)
        {
            for (int i = 0; i < tokens; i++)
            {
                InstantiateForComponent(out Token token, _tokenPrefab, _origin);
                _tokens.Add(token);
                token.Descend();
            }
            RearrangeTokenPositions();
        }

        private void RearrangeTokenPositions()
        {
            const float Spacing = 0.6f;
            
            for (int i = _tokens.Count - 1; i >= 0; i--)
            {
                _tokens[i].Descend();
                float z = -i * Spacing;
                Vector3 position = new Vector3(0, 0, z);
                _tokens[i].TargetLocalPosition = position;
            }
        }
    }
}