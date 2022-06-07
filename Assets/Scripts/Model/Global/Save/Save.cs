using System.IO;
using UnityEngine;

namespace Model.Global.Save
{
    public class Save : MonoBehaviour
    {
        
        protected void SaveData<T>(T data, string fileName) where T : SavedData
        {
            string text = JsonUtility.ToJson(data);
            File.WriteAllText(FileNameToPath(fileName), text);
        }

        protected bool TryLoadData<T>(out T data, string fileName) where T : SavedData
        {
            if (!File.Exists(FileNameToPath(fileName)))
            {
                data = null;
                return false;
            }

            string text = File.ReadAllText(FileNameToPath(fileName));
            data = JsonUtility.FromJson<T>(text);
            return true;
        }

        private string FileNameToPath(string fileName) => $"{Application.dataPath}/{fileName}.json";
    }
}