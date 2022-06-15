using System;
using UnityEngine;
using Zenject;

namespace View
{
    public class DestroyOnLaunch : MonoBehaviour
    {
        [Inject]
        private void Awake()
        {
            Destroy(gameObject);
        }
    }
}