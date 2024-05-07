using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BGSoundScript : MonoBehaviour
{

    private string sceneName;
    public AudioSource menuMusic;
    public AudioSource fightMusic;
    private static BGSoundScript instance = null;

    public static BGSoundScript Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        SceneManager.activeSceneChanged += OnSceneChange;

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneChange(Scene currentScene, Scene nextScene)
    {
        // change music if necessary
        Debug.Log("Scene changed! Current scene: " + currentScene.name + ", Next scene: " + nextScene.name);
        sceneName = nextScene.name;
        if (sceneName == "Pantry_Arena" || sceneName == "Oven_Arena"
            || sceneName == "Stove_Arena" || sceneName == "Sink_Arena"
            || sceneName == "Counter_Arena")
        {
            if (menuMusic != null) {
                menuMusic.Stop();
            }
            fightMusic.Play();
        }
        else if (nextScene.name == "MainMenu")
        {
            if (fightMusic != null) {
                fightMusic.Stop();
            }
            menuMusic.Play();
        }
    }
}