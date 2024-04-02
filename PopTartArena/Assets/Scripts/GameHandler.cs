using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class GameHandler : MonoBehaviour
{

    private GameObject[] players;
    public static int numPlayers = 4;
    public static int[] playerHealth;
    public int StartPlayerHealth = 100;
    public static int gotTokens = 0;
    public GameObject tokensText;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    void Start()
    {

        sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "PrototypeScene")
        {
            // get GameObject of each player and add to an array
            GameObject p1 = GameObject.FindWithTag("player1");
            GameObject p2 = GameObject.FindWithTag("player2");
            GameObject p3 = GameObject.FindWithTag("player3");
            GameObject p4 = GameObject.FindWithTag("player4");

            players = new GameObject[4]{p1, p2, p3, p4};
            playerHealth = new int[4];

            // deactivate players if less than 4
            for(int i = numPlayers; i < 4; i++)
            {
                players[i].SetActive(false);
            }

            // initialize player health
            for(int i = 0; i < numPlayers; i++)
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

    public void ChooseNumPlayers()
    {
        SceneManager.LoadScene("NumPlayers");
    }

    public void SetNumPlayers(int num)
    {
        numPlayers = num;
        StartGame();
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