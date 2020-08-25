using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatScript : MonoBehaviour
{    
    public GameObject Right;
    public GameObject Left;

    public Sprite[] RightCats = new Sprite[2];
    public Sprite[] LeftCats = new Sprite[2];

    public AudioClip[] acs = new AudioClip[4];

    private GameCtrl Gctrl;

    private Vector2 rPos;
    private Vector2 lPos;

    private Vector2 allPos;

    private bool r;
    private bool l;

    private bool waitR;
    private bool waitL;
    private bool imageRandR;
    private bool imageRandL;
    private bool rSoundPlayed;
    private bool lSoundPlayed;

    private int rImage;
    private int lImage;

    private float speed;
    private float initPos;

    private float rTime;
    private float lTime;
    private bool isRTimeSet;
    private bool isLTimeSet;

    private void Start()
    {
        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();

        imageRandR = false;
        imageRandL = false;

        r = false;
        l = false;

        waitR = false;
        waitL = false;

        rSoundPlayed = false;
        lSoundPlayed = false;

        rImage = 0;
        lImage = 0;

        speed = 5f;

        rTime = Random.Range(5, 11);
        lTime = Random.Range(10, 21);
        isRTimeSet = true;
        isLTimeSet = true;

        rPos = Right.transform.position;
        lPos = Left.transform.position;

        initPos = Left.transform.position.x;
        allPos = Gctrl.Up.transform.position;


        StartCoroutine(WaitTimeR());
        StartCoroutine(WaitTimeL());
    }


    private void Update()
    {
        if (imageRandR == false)
        {
            rImage = Random.Range(0, 2);
            imageRandR = true;
        }
        if (imageRandL == false)
        {
            lImage = Random.Range(0, 2);
            imageRandL = true;
        }

        if (Gctrl.isStarted == true)
        {
            if (isRTimeSet == false)
            {
                rTime = Random.Range(5, 11);
                isRTimeSet = true;
            }

            if (isLTimeSet == false)
            {
                lTime = Random.Range(10, 21);
                isLTimeSet = true;
            }

            rPos = Right.transform.position;
            lPos = Left.transform.position;
            allPos = Gctrl.Up.transform.position;

            Right.GetComponent<SpriteRenderer>().sprite = RightCats.ElementAt(rImage);
            Left.GetComponent<SpriteRenderer>().sprite = LeftCats.ElementAt(lImage);


            // Right Cat Begin
            if (waitR == false)
            {
                if(rSoundPlayed == false && PlayerPrefs.GetInt("isMute") == 0)
                {
                    rSoundPlayed = true;
                    switch (rImage)
                    {
                        case 0:
                            gameObject.GetComponent<AudioSource>().PlayOneShot(acs.ElementAt(0));
                            break;
                        case 1:
                            gameObject.GetComponent<AudioSource>().PlayOneShot(acs.ElementAt(1));
                            break;
                        default:
                            Debug.LogError("System Error => CatScript : Wrong var rImgae [" + rImage + "]");
                            break;
                    }
                }

                if (rPos.x < allPos.x + initPos)
                {
                    Right.transform.Translate(new Vector2(Time.deltaTime * speed, 0));
                }

                if (rPos.y < allPos.y + 1 && r == false)
                {
                    Right.transform.Translate(new Vector2(0, Time.deltaTime * speed));
                }

                if (rPos.y > allPos.y - initPos && r == true)
                {
                    Right.transform.Translate(new Vector2(0, -(Time.deltaTime * speed)));
                }

                if (rPos.y > allPos.y)
                    r = true;

                if (rPos.x > allPos.x + initPos && rPos.y < allPos.y - initPos)
                {
                    Right.transform.position = new Vector2(allPos.x - initPos, allPos.y - initPos);
                    StartCoroutine(WaitTimeR());
                }
            }
            // Right Cat End



            // Left Cat Begin
            if(waitL == false)
            {
                if(lSoundPlayed == false && PlayerPrefs.GetInt("isMute") == 0)
                {
                    lSoundPlayed = true;
                    switch (lImage)
                    {
                        case 0:
                            gameObject.GetComponent<AudioSource>().PlayOneShot(acs.ElementAt(2));
                            break;
                        case 1:
                            gameObject.GetComponent<AudioSource>().PlayOneShot(acs.ElementAt(3));
                            break;
                        default:
                            Debug.LogError("System Error => CatScript : Wrong var lImgae [" + lImage + "]");
                            break;
                    }
                }
                if(lPos.x > allPos.x - initPos)
                {
                    Left.transform.Translate(new Vector2(-(Time.deltaTime * speed), 0));
                }

                if (lPos.y < allPos.y + 1 && l == false)
                {
                    Left.transform.Translate(new Vector2(0, Time.deltaTime * speed));
                }

                if (lPos.y > allPos.y - initPos && l == true)
                {
                    Left.transform.Translate(new Vector2(0, -(Time.deltaTime * speed)));
                }

                if (lPos.y > allPos.y)
                    l = true;

                if (lPos.x < allPos.x - initPos && lPos.y < allPos.y - initPos)
                {
                    Left.transform.position = new Vector2(allPos.x + initPos, allPos.y - initPos);
                    StartCoroutine(WaitTimeL());
                }
            }
            // Left Cat End
        }
    }

    IEnumerator WaitTimeR()
    {
        Right.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        imageRandR = false;
        waitR = true;
        yield return new WaitForSeconds(rTime);
        r = false;
        waitR = false;
        Right.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        rSoundPlayed = false;
        isRTimeSet = false;
    }

    IEnumerator WaitTimeL()
    {
        Left.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        imageRandL = false;
        waitL = true;
        yield return new WaitForSeconds(lTime);
        l = false;
        waitL = false;
        Left.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        lSoundPlayed = false;
        isLTimeSet = false;
    }
}
