using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    public GameObject[] objects;

    public static DontDestroyOnLoadScene instance;

    void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        else 
            instance = this;

        foreach (GameObject element in objects)
            DontDestroyOnLoad(element);  
    }
}
