using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private Text coinText;

    public int coins = 0 ;

    public static Coin instance;

    public AudioSource audioSource;
    public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Coin")) {
            AudioSource.PlayClipAtPoint(sound, transform.position);
            coins += 1;
            AdaptativeDifficulty.coins += 1;
            if(AdaptativeDifficulty.actual_diff >= 70)
            {
                coins += 1;
                AdaptativeDifficulty.coins += 1;
            }
            
            coinText.text = "x" + coins;
            Destroy(collision.gameObject);
        }
    }
}