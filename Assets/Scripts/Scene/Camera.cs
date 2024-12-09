using UnityEngine;

public class Camera : MonoBehaviour
{
    public static Camera instance;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else 
            instance = this;
    }
}