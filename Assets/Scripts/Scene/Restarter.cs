using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        //modifie to die
        private Transform playerSpawn;

        private void Awake()
        {
            playerSpawn = GameObject.FindGameObjectWithTag("Respawn").transform;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch(collision.tag)
            {
                case "Player":
                    collision.transform.position = playerSpawn.position;
                    PlayerLife.instance.LoseHealth();
                    break;
                /*
                case "Knight":
                    Destroy(collision.gameObject);
                    break;
                    */
            }
        }
    }
}
