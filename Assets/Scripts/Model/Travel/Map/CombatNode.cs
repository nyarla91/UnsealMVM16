using Model.Global;
using UnityEngine;
using Zenject;

namespace Model.Travel.Map
{
    public class CombatNode : Node
    {
        [Inject] private SceneLoader _sceneLoader;
        
        protected override void OnPlayerEnter()
        {
            base.OnPlayerEnter();
            _sceneLoader.LoadCombat();
        }
    }
}