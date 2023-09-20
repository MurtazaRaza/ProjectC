using UnityEngine;

namespace Utils.BaseClasses
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T">Pass the name of this class so a proper typed static instance is created</typeparam>
    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        var obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}