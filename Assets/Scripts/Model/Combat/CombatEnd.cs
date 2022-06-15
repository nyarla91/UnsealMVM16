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
        [Inject] private ManualSave _manualSave;
        [Inject] private GlobalTravelState _travelState;
        [Inject] private SceneLoader _sceneLoader;
        [Inject] private Pause _pause;
        
        public void ExitVictory()
        {
            _pause.End(this);
            _permanentSave.Data.CombatsCleared.Add(_travelState.NextCombatData.Name);
            _travelState.NextCombatData.Reward.ClaimReward(_permanentSave, _manualSave);
            _travelState.PlayerHealth = _player.Health;
            _permanentSave.Save();
            _sceneLoader.LoadTravel();
        }

        public void ExitDeath()
        {
            _pause.End(this);
            _travelState.Reset();
            _sceneLoader.LoadTravel();
        }
    }
}