using UnityEngine;
using System.Collections;

public class PlayerEffects : MonoBehaviour
{
    public static PlayerEffects instance;

    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        else 
            instance = this;
    }

    public void AddSpeed(int speedGiven, float speedDuration)
    {
        PlayerMovement.instance.MovementSpeed += speedGiven;
        StartCoroutine(RemoveSpeed(speedGiven, speedDuration));
    }

    public IEnumerator RemoveSpeed(int speedGiven, float speedDuration)
    {
        yield return new WaitForSeconds(speedDuration);
        PlayerMovement.instance.MovementSpeed -= speedGiven;
    }
}
