using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            instance = FindFirstObjectByType<T>();
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                instance = gameObject.AddComponent<T>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this as T;
    }
}
