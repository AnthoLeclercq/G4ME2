using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerLife : MonoBehaviour
{

    //[SerializeField] private float size;

    public static Rigidbody2D rb;
    private Animator anim;

    public int currentLive;
    public int maxLives = 3;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private int armor = 0;
    private bool isInvincible = false;

    public static PlayerLife instance;
    private void Awake()
    {
        if (instance != null && instance != this) 
            Destroy(gameObject);
        else 
            instance = this;
    }

    void Start()
    {
        currentLive = maxLives;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        foreach (Image img in hearts)
            img.sprite = emptyHeart;
            
        for(int i = 0; i < currentLive; i++)
            hearts[i].sprite = fullHeart;
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        switch(collision.gameObject.tag)
        {
            case "Knight":
                LoseHealth();
                break;
            case "Alien":
                LoseHealth();
                break;
        }
    }

    //Lose health depending on the difficulty and the player becomes temporaly invincible during 3s
    public void LoseHealth() 
    {
        if (isInvincible)
            return;
        
        if (AdaptativeDifficulty.actual_diff >= 75)
        {
            currentLive -= 2;
            AdaptativeDifficulty.damage_taken += 2;
        }
        else
        {
            currentLive -= 1;
            AdaptativeDifficulty.damage_taken += 1;
        }

        if (currentLive <= 0) {
                Die();
                return;
        }
        StartCoroutine(BecomeTemporarilyInvincible(3));
    }

    //Health losed by the boss enraged.
    public void LoseEnragedHealth() 
    {
        if (isInvincible)
            return;
        
        currentLive -= 2;
        AdaptativeDifficulty.damage_taken += 2;

        if (currentLive <= 0) {
                Die();
                return;
        }
        StartCoroutine(BecomeTemporarilyInvincible(3));
    }

    public IEnumerator BecomeTemporarilyInvincible(float invincibilityDurationSeconds)
    {
        isInvincible = true;
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        isInvincible = false;
    } 


    public void Die() 
    {
        RestartGame();
        AdaptativeDifficulty.CallMeDiffDaddy();
        currentLive = maxLives;
        Inventory.instance.content.Clear();
        Inventory.instance.UpdateInventoryImage();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
