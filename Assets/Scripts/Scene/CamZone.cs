using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CamZone : MonoBehaviour
{
    [SerializeField] private Transform posZone; 

    private new Camera camera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If a player enter in a new map, the cam switch too
        switch(collision.tag)
        {
            case "Player":
                camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
                camera.transform.position = new Vector3(posZone.position.x, posZone.position.y, transform.position.z -10);
                AdaptativeDifficulty.rooms += 1;
                break;
        }
    }
}
