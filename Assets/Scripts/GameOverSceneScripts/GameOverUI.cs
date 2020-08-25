using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text topScoreText;
    public Text middleScoreText;
    public Text bestScoreText;

    public AudioSource resultSound;

    void Start()
    {
        if (PlayerPrefs.GetInt("isMute") == 0)
            resultSound.Play();
        middleScoreText.text = "Score :\n" + PlayerPrefs.GetInt("userScore").ToString("N0");
        bestScoreText.text = "Best Score :\n" + PlayerPrefs.GetInt("bestScore").ToString("N0");

        PlayerPrefs.DeleteKey("userScore");
    }

    public void OnBacktoTitle()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
