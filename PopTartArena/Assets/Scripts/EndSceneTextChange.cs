using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneTextChange : MonoBehaviour{

    public string endText;
    public void ChangeWinner(int winner)
    {
        Debug.Log("setting end winner text");
        Text theText = GetComponent<Text>();
        theText.text = "PLAYER " + winner + " WINS!!";

        endText = "Winner = " + winner;
    }
}
