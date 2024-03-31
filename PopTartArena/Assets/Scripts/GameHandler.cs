using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private GameObject[] player;
    public static int numPlayers;
    public static int[] playerHealth;
    public int StartPlayerHealth = 100;
    public GameObject healthText;

    public static int gotTokens = 0;
    public GameObject tokensText;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "MainMenu")
        {
            for (int i = 0; i < numPlayers; i++)
            {
                playerHealth[i] = StartPlayerHealth;
            }
        }
        updateStatsDisplay();
    }

    public void playerGetTokens(int newTokens)
    {
        gotTokens += newTokens;
        updateStatsDisplay();
    }

    public void playerGetHit(int damage, int whichPlayer)
    {
        int playerIndex = whichPlayer - 1;
        playerHealth[playerIndex] -= damage;
        if (playerHealth[playerIndex] >= 0)
        {
            updateStatsDisplay();
        }
        if (damage > 0)
        {
            //player.GetComponent<PlayerHurt>().playerHit();       //play GetHit animation
        }


        if (playerHealth[playerIndex] > StartPlayerHealth)
        {
            playerHealth[playerIndex] = StartPlayerHealth;
            updateStatsDisplay();
        }

        if (playerHealth[playerIndex] <= 0)
        {
            playerHealth[playerIndex] = 0;
            updateStatsDisplay();
            playerDies();
        }
    }

    public void updateStatsDisplay()
    {
        Text healthTextTemp = healthText.GetComponent<Text>();
        healthTextTemp.text = "HEALTH: " + playerHealth;
    }

    public void playerDies()
    {
        //player.GetComponent<PlayerHurt>().playerDead();       //play Death animation
        StartCoroutine(DeathPause());
    }

    IEnumerator DeathPause()
    {
        // player.GetComponent<PlayerMove>().isAlive = false;   //deactivate movement animation
        // player.GetComponent<PlayerJump>().isAlive = false;   //deactivate jump animation
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("EndLose");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("PrototypeScene");
    }

    // Return to MainMenu
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        // Reset all static variables here, for new games:
        for (int i = 0; i < numPlayers; i++)
        {
            playerHealth[i] = StartPlayerHealth;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
}