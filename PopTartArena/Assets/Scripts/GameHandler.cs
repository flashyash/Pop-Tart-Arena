using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Numerics;

public class GameHandler : MonoBehaviour
{

    private GameObject[] players;
    public static int numPlayers = 3;
    public static int level = 1;
    public static int[] playerHealth;
    public int StartPlayerHealth = 100;
    public float hitDisabledTime = 0.5f;
    public AudioSource deathSound;
    public AudioSource gotHit;
    public AudioSource fight;  
    private GameObject p1;
    private GameObject p2;
    private GameObject p3;

    private Animator p1Anim;
    private Animator p2Anim;
    private Animator p3Anim;
    private Slider healthSlider;

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;

        // This should actually run on all scenes with players in them ie non-menu scenes
        if (sceneName == "Pantry_Arena" || sceneName == "Oven_Arena"
            || sceneName == "Stove_Arena" || sceneName == "Sink_Arena"
            || sceneName == "Counter_Arena" || sceneName == "WillTest")
        {
            // get GameObject of each player and add to an array for ez access
            p1 = GameObject.FindWithTag("player1");
            p2 = GameObject.FindWithTag("player2");
            p3 = GameObject.FindWithTag("player3");

            p1Anim = p1.GetComponent<Animator>();
            p2Anim = p2.GetComponent<Animator>();
            p3Anim = p3.GetComponent<Animator>();

            players = new GameObject[3] { p1, p2, p3 };
            playerHealth = new int[3];

            // deactivate players if less than 4
            for (int i = numPlayers; i < 3; i++)
            {
                players[i].SetActive(false);
            }

            // initialize player health
            for (int i = 0; i < numPlayers; i++)
            {
                playerHealth[i] = StartPlayerHealth;
            }

            fight.Play();
        }
    }

    public void checkIfPlayerWon() {
        int playerWon = -1;
        for (int i = 0; i < playerHealth.Length; i++) {
            playerWon = checkWinCondition(i);
            Debug.Log("checked win condition and player won is " + playerWon);

            if (playerWon != -1) {
            SceneManager.LoadScene("EndScene");
        }
        }
    }

    public void playerGetHit(int damage, int whichPlayer)
    {
        int playerIndex = whichPlayer - 1;
        playerHealth[playerIndex] -= damage;

        if (playerHealth[playerIndex] > 0)
        {
            if (whichPlayer == 1)
            {
                Color og = p1.GetComponent<SpriteRenderer>().color;
                p1.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                StartCoroutine(playerRed(p1, og));
                StartCoroutine(PlayerDisabled(p1));
                p1Anim.Play("hit");
                gotHit.Play();
            }
            else if (whichPlayer == 2)
            {
                Color og = p2.GetComponent<SpriteRenderer>().color;
                p2.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                StartCoroutine(playerRed(p2, og));
                StartCoroutine(PlayerDisabled(p2));
                p2Anim.Play("hit");
                gotHit.Play();
            }
            else if (whichPlayer == 3)
            {
                Color og = p3.GetComponent<SpriteRenderer>().color;
                p3.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                StartCoroutine(playerRed(p3, og));
                StartCoroutine(PlayerDisabled(p3));
                p3Anim.Play("hit");
                gotHit.Play();
            }
        }


        if (playerHealth[playerIndex] > StartPlayerHealth)
        {
            playerHealth[playerIndex] = StartPlayerHealth;
        }
        // if player is dead
        if (playerHealth[playerIndex] <= 0)
        {
            playerHealth[playerIndex] = 0;
            if (whichPlayer == 1)
            {
                p1Anim.Play("death");
                deathSound.Play();
                StartCoroutine(DeathPause(p1));
            }
            else if (whichPlayer == 2)
            {
                p2Anim.Play("death");
                deathSound.Play();
                StartCoroutine(DeathPause(p2));
            }
            else if (whichPlayer == 3)
            {
                p3Anim.Play("death");
                deathSound.Play();
                StartCoroutine(DeathPause(p3));
            }
            checkIfPlayerWon();
        }
    }

public int checkWinCondition(int currentPlayerIndex)
{
    // Check if the current player's health is greater than 0
    if (playerHealth[currentPlayerIndex] > 0)
    {
        // Check if all other players have 0 health
        for (int j = 0; j < playerHealth.Length; j++)
        {
            if (j != currentPlayerIndex && playerHealth[j] > 0)
            {
                // If any other player has health greater than 0, the current player hasn't won yet
                return -1; // No winner yet
            }
        }
        // If all other players have 0 health, the current player has won
        return currentPlayerIndex;
    }
    // If the current player's health is 0 or less, they cannot win
    return -1; // No winner yet
}

    // void updateHealthBar(int whichPlayer)
    // {
    //     GameObject currPlayer = players[whichPlayer-1];
    //     healthSlider = currPlayer.GetComponentInChildren<Slider>();
    //     healthSlider.value = playerHealth[whichPlayer-1];
    // }

    IEnumerator playerRed(GameObject player, Color original) {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<SpriteRenderer>().material.SetColor("_Color", original); 
    }
    IEnumerator DeathPause(GameObject player)
    {
        // player.GetComponent<PlayerMove>().isAlive = false;   //deactivate movement animation
        // player.GetComponent<PlayerJump>().isAlive = false;   //deactivate jump animation
        player.GetComponent<MultiPlayerMoveAround>().isAlive = false;
        player.GetComponent<PlayerJump>().isAlive = false;
        player.GetComponent<PlayerShoot>().isAlive = false;
        player.GetComponent<PlayerAttackMelee>().isAlive = false;
        yield return new WaitForSeconds(2.0f); // make all death animations same length and match with this time
        player.SetActive(false);
    }

    IEnumerator PlayerDisabled(GameObject player)
    {
        // make player jump a lil bit, kinda like a knockback
        player.GetComponent<PlayerJump>().Jump(UnityEngine.Vector2.up / 3);
        // disable the player's ability to do stuff for a small time
        player.GetComponent<MultiPlayerMoveAround>().isAlive = false;
        player.GetComponent<PlayerJump>().isAlive = false;
        player.GetComponent<PlayerShoot>().isAlive = false;
        player.GetComponent<PlayerAttackMelee>().isAlive = false;
        yield return new WaitForSeconds(hitDisabledTime);
        player.GetComponent<MultiPlayerMoveAround>().isAlive = true;
        player.GetComponent<PlayerJump>().isAlive = true;
        player.GetComponent<PlayerShoot>().isAlive = true;
        player.GetComponent<PlayerAttackMelee>().isAlive = true;
    }

    public void ChooseNumPlayers()
    {
        SceneManager.LoadScene("NumPlayers");
    }

    public void SetNumPlayers(int num)
    {
        numPlayers = num;
        SceneManager.LoadScene("ChooseLevelScene");
    }

    public void SetLevel(int chosenLevel)
    {
        level = chosenLevel;
        LoadPrePlayScene();
    }

    public void LoadPrePlayScene()
    {
        SceneManager.LoadScene("PrePlay");
    }

    public void ShowInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ShowTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void StartGame()
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Pantry_Arena");
                break;
            case 2:
                SceneManager.LoadScene("Oven_Arena");
                break;
            case 3:
                SceneManager.LoadScene("Stove_Arena");
                break;
            case 4:
                SceneManager.LoadScene("Sink_Arena");
                break;
            case 5:
                SceneManager.LoadScene("Counter_Arena");
                break;
        }
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
    SceneManager.LoadScene("MainMenu");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}