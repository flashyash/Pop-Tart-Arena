using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

/*BASELINE PAUSEHANDLER*/
public class PauseHandler : MonoBehaviour {
        public AudioMixer mixer;
        public static float volumeLevel = 1.0f;
        private Slider sliderVolumeCtrl;
        public static bool isGamePaused = false;
        public GameObject pauseMenuUI;
        /*UNCOMMENT NEXT 2 LINES FOR TWEENING*/
        //public Tween twr;
        //public Tween twq;

        /*UNCOMMENT BELOW TO MAKE VOLUME SLIDER*/
        // public AudioMixer mixer;
        // public static float volumeLevel = 1.0f;
        // private Slider sliderVolumeCtrl;

       void Awake (){
                SetLevel (volumeLevel);
                GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = volumeLevel;
                }
        }

        void Start(){
                pauseMenuUI.SetActive(false);
                isGamePaused = false;
        }

        void Update(){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (isGamePaused){
                                Resume();
                        }
                        else{
                                Pause();
                        }
                }
        }

        public void Pause(){
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                /*UNCOMMENT BELOW FOR TWEENING*/
                //twr.ButtonPop();
                //twq.ButtonPop();
                isGamePaused = true;
        }

        public void Resume(){
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                isGamePaused = false;
        }

        public void QuitGame(){
                SceneManager.LoadScene("MainMenu");
                #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
                #else
                Application.Quit();
                #endif
        }

        // Return to MainMenu
        public void RestartGame()
        {
                Time.timeScale = 1f;
                SceneManager.LoadScene("MainMenu");
                // Reset all static variables here, for new games:
        }

         public void SetLevel (float sliderValue){
                mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
                volumeLevel = sliderValue;
        }
}