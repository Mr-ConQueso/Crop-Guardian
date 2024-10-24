using System.Collections.Generic;
using UnityEngine;

namespace BaseGame
{
    public static class HelperFunctions
    {
        /// <summary>
        /// Get the transform of the first object found with the selected tag
        /// </summary>
        /// <param name="tag">The tag to search for</param>
        /// <param name="transform">The final transform, if found</param>
        /// <returns></returns>
        public static bool TryGetTransformWithTag(string tag, out Transform transform)
        {
            GameObject obj = GameObject.FindWithTag(tag);
            if (obj != null)
            {
                transform = obj.transform;
                return true;
            }
            else
            {
                transform = null;
                return false;
            }
        }
        
        /// <summary>
        /// Changes the layer of the object and all its children
        /// </summary>
        /// <param name="obj">The object to change</param>
        /// <param name="newLayer">The new layer to apply</param>
        public static void ChangeLayerRecursively(GameObject obj, int newLayer)
        {
            obj.layer = newLayer;
            foreach (Transform child in obj.transform)
            {
                ChangeLayerRecursively(child.gameObject, newLayer);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetComponentInChild<T>(this GameObject parent, out T component) where T : Component
        {
            component = parent.GetComponentInChildren<T>();

            return component != null;
        }
        
        /// <summary>
        /// Finds all components of type T in the children of the parent GameObject.
        /// </summary>
        /// <typeparam name="T">The type of component to find.</typeparam>
        /// <param name="parent">The parent GameObject.</param>
        /// <returns>List of components found in the children.</returns>
        public static List<T> GetComponentsInChildren<T>(this GameObject parent) where T : Component
        {
            List<T> components = new List<T>();
    
            T[] foundComponents = parent.GetComponentsInChildren<T>();

            if (foundComponents != null && foundComponents.Length > 0)
            {
                components.AddRange(foundComponents);
            }

            return components;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetComponentInChild<T>(this Transform parent, out T component) where T : Component
        {
            component = parent.GetComponentInChildren<T>();

            return component != null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool CompareLayer(this GameObject obj, int layer)
        {
            return obj.layer == layer;
        }

        public static bool CompareLayer(this GameObject obj, string layerName)
        {
            return obj.layer == LayerMask.NameToLayer(layerName);
        }
        
        public static bool HasChildren(GameObject obj)
        {
            return obj.transform.childCount > 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockMode"></param>
        /// <param name="isVisible"></param>
        public static void SetCursorMode(CursorLockMode lockMode, bool isVisible)
        {
            Cursor.lockState = lockMode;
            Cursor.visible = isVisible; 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatTimer(float time)
        {
            // Format the elapsed time as minutes:seconds:milliseconds
            string minutes = Mathf.Floor(time / 60).ToString("00");
            string seconds = Mathf.Floor(time % 60).ToString("00");
            //string milliseconds = Mathf.Floor((elapsedTime * 1000) % 1000).ToString("000");

            return minutes + ":" + seconds;
        }
    }
}