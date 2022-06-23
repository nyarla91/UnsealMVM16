using System.Collections;
using UnityEngine;
using View;

namespace Presenter
{
    public class QuitGamePresenter : MonoBehaviour
    {
        [SerializeField] private UIDialog _view;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                StartCoroutine(Quit());

        }

        private IEnumerator Quit()
        {
            for (float i = 0; i < 1; i += Time.deltaTime)
            {
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    _view.FadeIn();
                    yield return new WaitForSeconds(1);
                    _view.FadeOut();
                    yield break;
                }
                yield return null;
            }
            print("Quit");
            Application.Quit();
        }
    }
}