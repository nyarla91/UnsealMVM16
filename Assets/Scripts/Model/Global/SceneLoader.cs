using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.Global
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadCombat() => LoadScene("Combat");
        public void LoadTravel() => LoadScene("Travel");

        private void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}