using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCtrl : MonoBehaviour
{
    public GameObject Level1Can;
    public GameObject Level2Can;
    public GameObject Level3Can;
    public GameObject GuideCan;
    public GameObject Up;
    public GameObject ChangeImage;
    public Text FeverText;
    public GameObject FeverTextObj;
    public GameObject FeverImage;
    public GameObject FeverImageText;
    public GameObject FeverAnimImage;

    [HideInInspector]
    public bool UserTouch;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public bool isTouch;
    [HideInInspector]
    public int Score;
    [HideInInspector]
    public bool isFeverTime;
    [HideInInspector]
    public int CanCount;
    [HideInInspector]
    public int layer;
    [HideInInspector]
    public int tmpCount;
    [HideInInspector]
    public int canCountForBack;
    [HideInInspector]
    public bool isStarted;
    

    private CanMove CM;
    private UICtrl Uctrl;
    private bool isRandInit;
    private int rand;
    private bool isFeverOver;
    private int feverTime;
    private bool isTouchAva;

    void Start()
    {
        isTouchAva = true;
        isStarted = false;
        feverTime = 5;
        canCountForBack = 0;
        isRandInit = false;
        isFeverOver = false;
        tmpCount = 0;
        CanCount = 0;
        isFeverTime = false;
        isTouch = true;
        level = 1;
        UserTouch = true;
        layer = 0;
        Score = 0;

        CM = GameObject.Find("Pos").GetComponent<CanMove>();
        Uctrl = GameObject.Find("EventSystem").GetComponent<UICtrl>();
        
    }

    void Update()
    {
        if (Score > 0)
        {
            isStarted = true;
            Destroy(GuideCan.gameObject);
        }
        if (!isRandInit)
        {
            rand = Random.Range(1, 4);
        }
        
        // Can Image Chane

        switch (rand)
        {
            case 1:
                ChangeImage.GetComponent<SpriteRenderer>().sprite = Level1Can.GetComponent<SpriteRenderer>().sprite;
                isRandInit = true;
                break;
            case 2:
                ChangeImage.GetComponent<SpriteRenderer>().sprite = Level2Can.GetComponent<SpriteRenderer>().sprite;
                isRandInit = true;
                break;
            case 3:
                ChangeImage.GetComponent<SpriteRenderer>().sprite = Level3Can.GetComponent<SpriteRenderer>().sprite;
                isRandInit = true;
                break;
            default:
                Debug.LogError("System Error in GameCtrl Script : [var : rand = " + rand + "]");
                break;
        }


        // FeverTime (Stack 30times)
        if (CanCount == 30)
        {
            
            StartCoroutine(FeverTime());
            isFeverTime = true;

            FeverImage.SetActive(true);
            FeverImageText.SetActive(true);
            FeverAnimImage.SetActive(true);

            Level1Can.GetComponent<Rigidbody2D>().gravityScale = 8;
            Level2Can.GetComponent<Rigidbody2D>().gravityScale = 8;
            Level3Can.GetComponent<Rigidbody2D>().gravityScale = 8;
            CanCount = 0;
        }

        if (isFeverTime == false && isFeverOver == true)
        {
            FeverImage.SetActive(false);
            FeverImageText.SetActive(false);
            FeverAnimImage.SetActive(false);

            Level1Can.GetComponent<Rigidbody2D>().gravityScale = 2;
            Level2Can.GetComponent<Rigidbody2D>().gravityScale = 2;
            Level3Can.GetComponent<Rigidbody2D>().gravityScale = 2;
            isFeverOver = false;
        }



        // User Touched
        if (Input.touchCount > 0)
        {
            // Touch Separate
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            { // UI Touched
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
            else // GameDisplay Touched
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        
                        if (UserTouch == true && isTouch == true && isTouchAva == true)
                        {
                            if (PlayerPrefs.GetInt("isMute") == 0)
                                Uctrl.CanSound.Play();

                            ++layer;
                            ++tmpCount;
                            UserTouch = false;

                            switch (rand)
                            {
                                case 1:
                                    Instantiate(Level1Can).transform.position = CM.transform.position;
                                    isRandInit = false;
                                    break;
                                case 2:
                                    Instantiate(Level2Can).transform.position = CM.transform.position;
                                    isRandInit = false;
                                    break;
                                case 3:
                                    Instantiate(Level3Can).transform.position = CM.transform.position;
                                    isRandInit = false;
                                    break;
                                default:
                                    Debug.LogError("System Error in GamCtrl Script");
                                    break;
                            }
                        }
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

    IEnumerator FeverTime()
    {
        FeverTextObj.SetActive(true);
        for(int i = 5; i > 0; i--)
        {
            FeverText.text = feverTime + " Sec";
            feverTime -= 1;
            yield return new WaitForSeconds(1f);
        }
        
        isFeverTime = false;
        isFeverOver = true;
        isTouchAva = false;
        FeverText.text = "";

        FeverTextObj.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        feverTime = 5;
        isTouchAva = true;
    }

}