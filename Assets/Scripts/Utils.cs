
    using System.Linq;
    using UnityEngine;

    public static class Utils
    {
       
        public static bool HasComponent<T>(this GameObject go) where T : Component
        {
            return go.GetComponentsInChildren<T>().FirstOrDefault() != null;
        }
    }
