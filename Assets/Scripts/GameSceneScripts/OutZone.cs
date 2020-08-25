using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutZone : MonoBehaviour
{
    GameCtrl Gctrl;

    private void Start()
    {
        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Gctrl.Score -= 100;
        SceneManager.LoadScene("GameOverScene");
        PlayerPrefs.SetInt("userScore", Gctrl.Score);
        if (PlayerPrefs.GetInt("userScore") > PlayerPrefs.GetInt("bestScore"))
            PlayerPrefs.SetInt("bestScore", PlayerPrefs.GetInt("userScore"));
    }
}
