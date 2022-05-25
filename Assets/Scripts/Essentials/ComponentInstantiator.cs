using System;
using UnityEngine;

namespace Essentials
{
    public class ComponentInstantiator : Transformer
    {
        protected void InstantiateForComponent<T>(out T component, T prefab)
            where T : MonoBehaviour => InstantiateForComponent(out component, prefab, Vector3.zero);
        
        protected void InstantiateForComponent<T>(out T component,T prefab, Vector3 position)
            where T : MonoBehaviour => InstantiateForComponent(out component, prefab, position, null);
        
        protected void InstantiateForComponent<T>(out T component,T prefab, Vector3 position, Transform parent)
            where T : MonoBehaviour => InstantiateForComponent(out component, prefab, position, Quaternion.identity, parent);
        protected void InstantiateForComponent<T>(out T component,T prefab, Transform parent)
            where T : MonoBehaviour => InstantiateForComponent(out component, prefab, parent.position, Quaternion.identity, parent);
        
        protected void InstantiateForComponent<T>(out T component,T prefab, Vector3 position, Quaternion rotation)
            where T : MonoBehaviour => InstantiateForComponent(out component, prefab, position, rotation, null);
        
        protected void InstantiateForComponent<T>(out T component,T prefab, Vector3 position, Quaternion rotation, Transform parent)
            where T : MonoBehaviour
        {
            component = Instantiate(prefab.gameObject, position, rotation, parent).GetComponent<T>();
        }
    }
}