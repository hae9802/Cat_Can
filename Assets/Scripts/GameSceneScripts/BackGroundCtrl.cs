using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundCtrl : MonoBehaviour
{
    public GameObject BackImage;
    public GameObject[] Cloud = new GameObject[5];
    public Sprite[] back = new Sprite[9];

    [HideInInspector]
    public bool isTouched;
    [HideInInspector]
    public bool isCloudOff;
    [HideInInspector]
    public bool isBackGroundEnd;

    private GameCtrl Gctrl;

    private int tmp;


    void Start()
    {
        isBackGroundEnd = false;
        isCloudOff = true;
        isTouched = false;
        tmp = 0;

        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
        BackImage.GetComponent<SpriteRenderer>().sprite = back[0];
    }

    void Update()
    {
        if (isCloudOff == true && isBackGroundEnd == false)
        {
            Instantiate(Cloud[0]).transform.position = new Vector2(0, Gctrl.Up.transform.position.y + 10);
            isCloudOff = false;
        }

        if (tmp % 2 == 0 && Gctrl.canCountForBack % 60 == 0 && Gctrl.canCountForBack != 0 && isTouched == true && Gctrl.isFeverTime == false)
        {
            Gctrl.canCountForBack = 0;
            if (tmp < back.Length - 1)
                ++tmp;
            BackImage.GetComponent<SpriteRenderer>().sprite = back[tmp];
            isTouched = false;
        }

        if(tmp % 2 != 0 && isTouched == true)
        {
            Gctrl.canCountForBack = 0;
            ++tmp;
            BackImage.GetComponent<SpriteRenderer>().sprite = back[tmp];
            isTouched = false;
        }
    }
}
