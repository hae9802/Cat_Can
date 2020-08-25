using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// PlayerPrefs 
// isMute : Music On Off var ( 0 is on, 1 is off)
// userScore : Save user score for GameOverScene, if game over it is deleted
// bestScore : Save best score var, it is not deleted until user clicked reset button
 //Volume : Music Volume for Share, if Build to.apk delete script


public class MainUI : MonoBehaviour
{
    public AudioSource Music;
    public GameObject OptionCanvas;
    public GameObject MainCanvas;

    public Text resetText;

    void Start()
    {
        PlayerPrefs.SetFloat("Volume", 0.5f);
        PlayerPrefs.GetInt("isMute", 0);
        MainCanvas.SetActive(true);
        OptionCanvas.SetActive(false);
        Music.volume = PlayerPrefs.GetFloat("Volume");
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("isMute") == 0)
            Music.mute = false;
        else if (PlayerPrefs.GetInt("isMute") == 1)
            Music.mute = true;

        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Ended:
                        break;
                    case TouchPhase.Canceled:
                        break;
                }
            }
            else
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (Application.platform == RuntimePlatform.Android)
                        {
                            if (Input.GetKeyDown(KeyCode.Escape))
                                Application.Quit();
                            else
                                SceneManager.LoadScene("GameScene");
                        }
                        else
                            SceneManager.LoadScene("GameScene");
                        break;
                    case TouchPhase.Moved:
                        break;
                    case TouchPhase.Ended:
                        break;
                    case TouchPhase.Canceled:
                        break;
                }
            }
        }
    }

    public void OnMusicOnClicked()
    {
        PlayerPrefs.SetInt("isMute", 0);
    }

    public void OnMusicOffClicked()
    {
        PlayerPrefs.SetInt("isMute", 1);
    }

    public void OnOptionClicked()
    {
        OptionCanvas.SetActive(true);
        MainCanvas.SetActive(false);
    }

    public void OnBackclicked()
    {
        OptionCanvas.SetActive(false);
        MainCanvas.SetActive(true);
        //resetText.text = "";
    }

    public void OnTouchText()
    {
        SceneManager.LoadScene("GameScene");
    }

    //public void OnResetClicked()
    //{
    //    PlayerPrefs.DeleteKey("bestScore");
    //    resetText.text = "점수가 초기화 되었습니다.";
    //}

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
