using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{

    private GameObject[] players;
    public static int numPlayers = 3;
    public static int level = 1;
    public static int[] playerHealth;
    public int StartPlayerHealth = 100;
    public AudioSource deathSound;
    public AudioSource gotHit;
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
        if (sceneName == "Pantry_Arena" || sceneName == "WillTest")
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
        }
    }

    public void playerGetHit(int damage, int whichPlayer)
    {
        int playerIndex = whichPlayer - 1;
        playerHealth[playerIndex] -= damage;

        if (whichPlayer == 1)
        {
            p1Anim.Play("hit");
            gotHit.Play();
        }
        else if (whichPlayer == 2)
        {
            p2Anim.Play("hit");
            gotHit.Play();
        }
        else if (whichPlayer == 3)
        {
            p3Anim.Play("hit");
            gotHit.Play();
        }

        updateHealthBar(whichPlayer);

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
        }
    }

    void updateHealthBar(int whichPlayer)
    {
        GameObject currPlayer = players[whichPlayer-1];
        healthSlider = currPlayer.GetComponentInChildren<Slider>();
        healthSlider.value = playerHealth[whichPlayer-1];
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

    public void StartGame()
    {
        switch (level)
        {
            case 1:
                SceneManager.LoadScene("Pantry_Arena");
                break;
            case 2:
                SceneManager.LoadScene("Pantry_Arena");
                break;
            case 3:
                SceneManager.LoadScene("Pantry_Arena");
                break;
            case 4:
                SceneManager.LoadScene("Pantry_Arena");
                break;
            case 5:
                SceneManager.LoadScene("Pantry_Arena");
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}