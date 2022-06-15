using System;
using System.Collections.Generic;
using Model.Combat.Characters;
using Model.Combat.Characters.Enemies;
using Model.Global;
using Model.Global.Save;
using Model.Travel.Map.Rewards;
using UnityEngine;
using Zenject;

namespace Model.Travel.Map
{
    public class CombatNode : NodeKind
    {
        [SerializeField] private Node _node;
        [SerializeField] private bool _dontSave;
        [SerializeField] private CombatData _data;
        
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private PermanentSave _permanentSave;
        [Inject] private GlobalTravelState _globalTravelState;

        private void Start()
        {
            if (!_dontSave && _permanentSave.Data.CombatsCleared.Contains(gameObject.name))
                _node.InteractionActive = false;
            _data.SetNameFromNode(this);
        }

        public override void OnPLayerEnter()
        {
            _globalTravelState.NextCombatData = _data;
            _sceneLoader.LoadCombat();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _data.Reward switch
            {
                CardReward => Color.white,
                KeyReward => Color.blue,
                DieReward => Color.yellow,
                FormReward => Color.red,
                _ => Gizmos.color
            };
            Gizmos.DrawSphere(gameObject.transform.position + Vector3.up, 0.5f);
        }
    }

    [Serializable]
    public class CombatData
    {
        [SerializeField] private CombatReward _reward;
        [SerializeField] private int _enemiesAtOnce;
        [SerializeField] private List<Enemy> _enemies;

        public int EnemiesAtOnce => _enemiesAtOnce;
        public List<Enemy> Enemies => _enemies;
        public CombatReward Reward => _reward;
        public string Name { get; private set; }

        public void SetNameFromNode(CombatNode node) => Name = node.gameObject.name;
    }
}