using Model.Travel.Map;
using UnityEngine;

namespace Model.Global
{
    public class GlobalTravelState : MonoBehaviour
    {
        public Vector3 CurrentNodePosition { get; set; }
        public CombatData NextCombatData { get; set; }
        public int PlayerHealth { get; set; }

        public void Reset()
        {
            CurrentNodePosition = Vector3.down;
            NextCombatData = null;
            PlayerHealth = 25;
        }
        
        private void Awake()
        {
            Reset();
            DontDestroyOnLoad(gameObject);
        }
    }
}