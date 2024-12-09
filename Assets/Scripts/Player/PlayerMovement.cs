using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{   
    public float MovementSpeed;
    [SerializeField] private float JumpForce; 
    [SerializeField] private LayerMask jumpableGround;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D coll;

    public static float movement;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    public float cooldownTimeDash;
    private float nextAction;

    private bool isJumping;

    public Animator anim;

    private GameObject[] CanDown;

    private enum MovementState {idle, running, jumping, falling};

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        else 
            instance = this;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();       

        dashTime = startDashTime;
    }

    private void Update()
    {
        MovementUpdate();
        AnimationUpdate();
    }

    // Personalized functions
    public void MovementUpdate()
    {
        CanDown = GameObject.FindGameObjectsWithTag("CanDown");
        if (Input.GetButtonDown("Jump") && IsGrounded())
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        //system to fix bug jump on platform CanDown
        if (CanDown != null)
        {
            if (isJumping || Input.GetKey(KeyCode.DownArrow))
            {
                foreach (GameObject canDown in CanDown)
                    canDown.GetComponent<BoxCollider2D>().enabled = false;  
            }          
            else
            {
                foreach (GameObject canDown in CanDown)
                    canDown.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
             
        if (direction == 0)
        {
            if(Time.time > nextAction)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    nextAction = Time.time + cooldownTimeDash;
                    if (movement < 0)
                        direction = 1;
                    else if (movement > 0)
                        direction = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                _rigidbody.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if (direction == 1)
                {
                    _rigidbody.velocity = Vector2.left * dashSpeed;
                    StartCoroutine(PlayerLife.instance.BecomeTemporarilyInvincible(1));           
                }   
                else
                {
                    _rigidbody.velocity = Vector2.right * dashSpeed;
                    StartCoroutine(PlayerLife.instance.BecomeTemporarilyInvincible(1));
                }   
            }
        }
    }

    private void AnimationUpdate()
    {
        MovementState state;

        if (movement > 0f) {
            state = MovementState.running;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movement < 0f) {
            state = MovementState.running;
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            state = MovementState.idle;
        }


        if (_rigidbody.velocity.y > 0f) {
            state = MovementState.jumping;
            isJumping = true;
        }
        else if (_rigidbody.velocity.y < 0f) {
            state = MovementState.falling;
            isJumping = false;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
