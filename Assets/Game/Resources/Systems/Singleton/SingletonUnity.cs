using UnityEngine;

public abstract class SingletonUnity<T> : MonoBehaviour where T : SingletonUnity<T>
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = (T)this;
            return;
        }
    }

}
