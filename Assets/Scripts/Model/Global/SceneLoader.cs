using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model.Global
{
    public class SceneLoader : MonoBehaviour
    {
        public const float TransitionTime = 0.4f;

        private Coroutine _sceneLoading; 
            
        public SceneTransition Transition { get; set; }
        
        public void LoadCombat() => LoadScene("Combat");
        public void LoadTravel() => LoadScene("Travel");
        public void LoadDeckbuilding() => LoadScene("Deckbuilding");


        private void LoadScene(string scene)
        {
            if (_sceneLoading != null)
                return;
            _sceneLoading = StartCoroutine(LoadSceneCoroutine(scene));
        }

        private IEnumerator LoadSceneCoroutine(string scene)
        {
            Transition?.FadeIn();
            yield return new WaitForSeconds(TransitionTime);
            _sceneLoading = null;
            SceneManager.LoadScene(scene);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}