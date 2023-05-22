using UnityEngine;
using System.Collections;

public abstract class CoreComponent<T> where T : MonoBehaviour {
    protected T player;
    public abstract void Update();
    public abstract void OnEnable();
    public abstract void onDisable();
}

