using UnityEngine;
using System.Collections.Generic;

public class OnSceneObjectsService
{
    public IEnumerable<T> GetSceneObjects<T>() where T: Component
    {
        return GameObject.FindObjectsOfType<T>();
    }

    public void RemoveSceneObjects<T>() where T: Component
    {
        T[] objects = GameObject.FindObjectsOfType<T>();
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject.Destroy(objects[i].gameObject);
        }
    }
}