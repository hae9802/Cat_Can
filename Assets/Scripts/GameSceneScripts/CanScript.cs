using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanScript : MonoBehaviour
{
    private Rigidbody2D rd2;
    private GameCtrl Gctrl;
    private BackGroundCtrl Bctrl;
    private CanMove CM;
    private UICtrl Uctrl;

    private float upSpeed;
    private bool init;
    private bool isSet;


    private void Start()
    {
        rd2 = GetComponent<Rigidbody2D>();

        isSet = false;
        init = true;

        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
        CM = GameObject.Find("Pos").GetComponent<CanMove>();
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = Gctrl.layer;
        Bctrl = GameObject.Find("BackGroundCtrl").GetComponent<BackGroundCtrl>();
        Uctrl = GameObject.Find("EventSystem").GetComponent<UICtrl>();
        if (Gctrl.isFeverTime == true)
            StartCoroutine(FeverCoroutine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 

        if (Gctrl.isFeverTime == false)
            upSpeed = 0.724f;
        else
            upSpeed = 0.7f;

        if (init == true) // Call Func Once
        {
            StartCoroutine(RemainFreeze());

            init = false;

            if (Gctrl.isFeverTime == false)
                ++Gctrl.canCountForBack;
            Gctrl.Score += 100; // Score UP
            Gctrl.UserTouch = true; // Touch Ctrl
            Bctrl.isTouched = true;

            if (Gctrl.tmpCount > 5)
            {
                Gctrl.Up.GetComponent<Transform>().Translate(0, upSpeed, 0); // Camera Up
            }

            


            if (Gctrl.isFeverTime == false) // Not FeverTime
            {
                Gctrl.CanCount++;
                CM.speed += 0.06f; // Can Moving Speed Up
            }
            else if (Gctrl.isFeverTime == true && isSet == true)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                rd2.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    IEnumerator RemainFreeze()
    {
        bool isFreeze = false;
        float tmp;
        tmp = gameObject.transform.position.x;
        yield return new WaitForSeconds(5.0f);
        if (tmp - gameObject.transform.position.x < 0.1f && gameObject.transform.position.x - tmp < 0.1f )
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            rd2.constraints = RigidbodyConstraints2D.FreezeAll;
            isFreeze = true;
        }

        if(isFreeze == false)
        {
            rd2.velocity = new Vector2(100f, 100f);
        }
    }

    IEnumerator FeverCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        if (init == true)
            isSet = false;
        else
            isSet = true;
    }
}
