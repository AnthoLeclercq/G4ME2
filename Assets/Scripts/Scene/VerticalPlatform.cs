using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float cooldownTimer;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (effector.rotationalOffset != 0f)
            effector.rotationalOffset = 0f;

        if(Input.GetKey(KeyCode.DownArrow))
            effector.rotationalOffset = 180f;
    }
}
