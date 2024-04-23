using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemHazard : MonoBehaviour
{
    private GameObject handler;
    private int playerNum;
    public int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        handler = GameObject.FindWithTag("GameHandler");
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == 6) //check if the other gameObject is in the enemies layer (6)
        {
            playerNum = int.Parse(other.gameObject.tag.Substring(6, 1)); // read the number of the player tag
            handler.GetComponent<GameHandler>().playerGetHit(damage, playerNum);
        }
    }
}
