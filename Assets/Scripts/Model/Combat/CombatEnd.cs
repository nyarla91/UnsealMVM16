using Model.Combat.Characters;
using Model.Global;
using Model.Global.Save;
using UnityEngine;
using Zenject;

namespace Model.Combat
{
    public class CombatEnd : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        [Inject] private PermanentSave _permanentSave;
        [Inject] private GlobalTravelState _travelState;
        [Inject] private SceneLoader _sceneLoader;
        
        public void ExitVictory()
        {
            _permanentSave.Data.CombatsCleared.Add(_travelState.NextCombatData.Name);
            _travelState.NextCombatData.Reward.ClaimReward(_permanentSave);
            _travelState.PlayerHealth = _player.Health;
            _permanentSave.Save();
            _sceneLoader.LoadTravel();
        }

        public void ExitDeath()
        {
            _travelState.Reset();
            _sceneLoader.LoadTravel();
        }
    }
}