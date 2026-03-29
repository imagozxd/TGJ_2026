using UnityEngine;

/// <summary>
/// When create a new instance, it overrides the current instance with the new one.
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;
}

/// <summary>
/// Basic singleton. This will destroy any new versions created, leaving the 
/// original instance intact
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour {
    protected override void Awake() {
        if (Instance != null){
            Destroy(this);
            return;
        }
        base.Awake();
    }
}

/// <summary>
/// Persistent version of the singleton. This will survive through scene loads.
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour {
    protected override void Awake() {
        base.Awake();
        if (Instance != this)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}