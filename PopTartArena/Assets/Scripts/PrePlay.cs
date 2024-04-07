using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrePlay : MonoBehaviour
{
    public Text levelText;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    // Start is called before the first frame update
    void Start()
    {
        int numPlayers = GameHandler.numPlayers;
        int level = GameHandler.level;

        switch (numPlayers)
        {
            case 1:
                Player1.SetActive(true);
                Player2.SetActive(false);
                Player3.SetActive(false);
                Player4.SetActive(false);
                break;
            case 2:
                Player1.SetActive(true);
                Player2.SetActive(true);
                Player3.SetActive(false);
                Player4.SetActive(false);
                break;
            case 3:
                Player1.SetActive(true);
                Player2.SetActive(true);
                Player3.SetActive(true);
                Player3.SetActive(false);
                break;
            case 4:
                Player1.SetActive(true);
                Player2.SetActive(true);
                Player3.SetActive(true);
                Player4.SetActive(true);
                break;
        }

        switch (level)
        {
            case 1:
                levelText.text = "Level 1: Stove";
                break;
            case 2:
                levelText.text = "Level 2: Oven";
                break;
            case 3:
                levelText.text = "Level 3: Drawer";
                break;
            case 4:
                levelText.text = "Level 4: Table";
                break;
            case 5:
                levelText.text = "Level 5: Counter";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
