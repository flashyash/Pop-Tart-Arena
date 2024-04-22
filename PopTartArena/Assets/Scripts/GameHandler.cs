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

    public bool isDefending = false;

    public static bool stairCaseUnlocked = false;
    //this is a flag check. Add to other scripts: GameHandler.stairCaseUnlocked = true;

    private string sceneName;
    public static string lastLevelDied;  //allows replaying the Level where you died

    void Start()
    {

        sceneName = SceneManager.GetActiveScene().name;

        // change later to run on all game scenes

        if (sceneName == "Pantry_Arena")
        {
            // get GameObject of each player and add to an array
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
        if (whichPlayer == 1)
        {
            p1Anim.Play("hit");
            gotHit.Play();
            updateHealthBar(1);
        }
        else if (whichPlayer == 2)
        {
            p2Anim.Play("hit");
            gotHit.Play();
            updateHealthBar(2);
        }
        else if (whichPlayer == 3)
        {
            p3Anim.Play("hit");
            gotHit.Play();
            updateHealthBar(3);
        }

        int playerIndex = whichPlayer - 1;
        playerHealth[playerIndex] -= damage;



        if (playerHealth[playerIndex] > StartPlayerHealth)
        {
            playerHealth[playerIndex] = StartPlayerHealth;
        }

        if (playerHealth[playerIndex] <= 0)
        {
            playerHealth[playerIndex] = 0;
            if (whichPlayer == 1)
            {
                Debug.Log("P1Died");
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

    public void updateHealthBar(int playerNumber)
    {
        Transform healthBar = players[playerNumber - 1].transform.Find("HealthBar");
        Transform zeroHits = healthBar.transform.Find("0Hits");
        Transform oneHit = healthBar.transform.Find("1Hit");
        Transform twoHits = healthBar.transform.Find("2Hits");
        Transform threeHits = healthBar.transform.Find("3Hits");
        Transform fourHits = healthBar.transform.Find("4Hits");

        if (zeroHits.gameObject.activeSelf)
        {
            zeroHits.gameObject.SetActive(false);
            oneHit.gameObject.SetActive(true);
        }

        else if (oneHit.gameObject.activeSelf)
        {
            oneHit.gameObject.SetActive(false);
            twoHits.gameObject.SetActive(true);
        }

        else if (twoHits.gameObject.activeSelf)
        {
            twoHits.gameObject.SetActive(false);
            threeHits.gameObject.SetActive(true);
        }

        else if (threeHits.gameObject.activeSelf)
        {
            threeHits.gameObject.SetActive(false);
            fourHits.gameObject.SetActive(true);
        }
    }


    IEnumerator DeathPause(GameObject player)
    {
        // player.GetComponent<PlayerMove>().isAlive = false;   //deactivate movement animation
        // player.GetComponent<PlayerJump>().isAlive = false;   //deactivate jump animation
        yield return new WaitForSeconds(1.0f);
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