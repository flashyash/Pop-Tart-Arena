using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardSpawner : MonoBehaviour
{
    
    public GameObject[] items;
    private float cooldown;
    private float currTime;
    public float lowRange = 2f;
    public float highRange = 10f;
    public GameObject warning;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(lowRange, highRange);
        currTime = 0f;
        warning.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(currTime >= cooldown) {
            //reset timers
            cooldown = Random.Range(lowRange, highRange);
            currTime = 0f; 
            //show warning
            warning.SetActive(true);
            StartCoroutine(warningFlash());
            //spawn a random object
            Instantiate(items[Random.Range(0, items.Length - 1)], transform.position, Quaternion.identity);
        }

        currTime += Time.deltaTime;  
    }

    IEnumerator warningFlash() {
        yield return new WaitForSeconds(2f);
        warning.SetActive(false);
    }
}
