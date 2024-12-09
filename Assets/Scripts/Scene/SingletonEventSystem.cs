using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEventSystem : MonoBehaviour
{
    public static SingletonEventSystem instance;

    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        else 
            instance = this;
    }
}
