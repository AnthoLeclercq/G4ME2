using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptativeDifficulty : MonoBehaviour
{
    

    [SerializeField] public static int actual_diff = 50;
    public static int monsters_killed = 0;
    public static bool boss_killed = false;
    public static int rooms = 0;
    public static int damage_taken = 0;
    public static int coins = 0;

    // Start is called before the first frame update
    public static AdaptativeDifficulty instance;

    public static void CallMeDiffDaddy()
    {
        //Debug.Log($"Boss killed : {boss_killed}");
        actual_diff = CalculDiff(actual_diff, monsters_killed, rooms, coins, damage_taken, boss_killed);
        monsters_killed = 0;
        damage_taken = 0;
        boss_killed = false;
        rooms = 0;
    }

    public static int CalculDiff(int actual_diff, int monsters, int rooms, int coins, int damages, bool boss_killed)
    {
        int playerScore = 0;
        int mediumScore = 6;
        //Debug.Log($"New difficulty : \n Actual diff : {actual_diff}\n Monsters : {monsters}\n Rooms : {rooms}\n Coins : {coins}\n Damages : {damages}\n Boss killed? {boss_killed}");

        if (monsters == 0 && rooms <= 2 && coins < 5) {
            if (actual_diff - 20  <= 0)
                return 1;
            return actual_diff - 20;
        }

        if (boss_killed) {
            if (actual_diff + 20  > 100)
                return 100;
            return actual_diff + 20;
        }           
        else
        {
            playerScore = (int)((actual_diff * 0.1 + monsters + rooms + coins * 0.5 - damages) / 5 - mediumScore); 
        }
        
        //Limite entre 0 et 100
        if (actual_diff + playerScore <= 0)
            return 1;
        else if (actual_diff + playerScore > 100)
            return 100;
        
        //Debug.Log($"New difficulty : {actual_diff + playerScore}");

        return actual_diff + playerScore;
    }
    

}
