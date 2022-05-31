using System;
using Model.Travel.Map;
using UnityEngine;

namespace Model.Global
{
    public class GlobalTravelState : MonoBehaviour
    {
        public Vector3 CurrentNodePosition { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}