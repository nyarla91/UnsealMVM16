using Model.Global;
using Model.Global.Save;
using UnityEngine;
using Zenject;

namespace Model.Zenject
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoader _sceneLoaderPrefab;
        [SerializeField] private GlobalTravelState _globalTravelStatePrefab;
        [SerializeField] private GlobalDeck _globalDeck;
        [SerializeField] private PermanentSave _permanentSavePrefab;
        [SerializeField] private ManualSave _manualSavePrefab;

        public override void InstallBindings()
        {
            BindFromPrefab(_globalDeck);
            BindFromPrefab(_globalTravelStatePrefab);
            BindFromPrefab(_sceneLoaderPrefab);
            BindFromPrefab(_permanentSavePrefab);
            BindFromPrefab(_manualSavePrefab);
        }

        private void BindFromPrefab<T>(T prefab) where T : MonoBehaviour
        {
            T instance = Container.InstantiatePrefab(prefab.gameObject).GetComponent<T>();
            Container.Bind<T>().FromInstance(instance).AsSingle();
        }
    }
}