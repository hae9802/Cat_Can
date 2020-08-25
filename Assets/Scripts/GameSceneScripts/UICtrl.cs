using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICtrl : MonoBehaviour
{
    public GameObject PauseCanvas;
    public GameObject OptionCanvas;
    public GameObject GameCanvas;

    public AudioSource MainMusic;
    public AudioSource FeverMusic;
    public AudioSource CanSound;

    public Text ScoreText;

    private GameCtrl Gctrl;

    void Start()
    {
        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
        GameCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
        OptionCanvas.SetActive(false);

        MainMusic.volume = PlayerPrefs.GetFloat("Volume");
        FeverMusic.volume = PlayerPrefs.GetFloat("Volume");

    }

    void Update()
    {
        ScoreText.text = "Score : " + Gctrl.Score.ToString("N0");
        if (PlayerPrefs.GetInt("isMute") == 0)
        {
            MainMusic.mute = false;
        }
        else
        {
            MainMusic.mute = true;
        }


        if(Gctrl.isFeverTime == true && PlayerPrefs.GetInt("isMute") == 0)
        {
            FeverMusic.mute = false;
            MainMusic.mute = true;
        }
        else if(Gctrl.isFeverTime == false && PlayerPrefs.GetInt("isMute") == 0)
        {
            FeverMusic.mute = true;
            MainMusic.mute = false;
        }

    }


    public void OnPauseClicked()
    {
        Gctrl.isTouch = false;
        PauseCanvas.SetActive(true);
        GameCanvas.SetActive(false);
        Time.timeScale = 0;
    }

    public void OnOptionClicked()
    {
        PauseCanvas.SetActive(false);
        OptionCanvas.SetActive(true);
    }

    public void OnResumeClicked()
    {
        PauseCanvas.SetActive(false);
        GameCanvas.SetActive(true);
        Time.timeScale = 1;
        Gctrl.isTouch = true;
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void OnBackClicked()
    {
        OptionCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    public void OnMusicOnClicked()
    {
        PlayerPrefs.SetInt("isMute", 0);
    }

    public void OnMusicOffClicked()
    {
        PlayerPrefs.SetInt("isMute", 1);
    }

    public void OnBacktoMainClicked()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}
