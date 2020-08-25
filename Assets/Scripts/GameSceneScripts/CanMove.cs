using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMove : MonoBehaviour
{
    public GameObject UpPosition;

    [HideInInspector]
    public float Pos;
    [HideInInspector]
    public float speed;

    private bool dir = false;
    private GameCtrl Gctrl;

    void Start()
    {
        Pos = 0;
        speed = 3.0f;

        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
    }

    void Update() 
    { 

        if (Gctrl.isFeverTime == false)
        {
            Pos = Time.deltaTime * speed;

            if (dir == false)
                transform.Translate(Pos, 0, 0);
            else
                transform.Translate(-Pos, 0, 0);

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            if (viewPos.x > 1f) dir = true;
            if (viewPos.x < 0f) dir = false;

            viewPos.x = Mathf.Clamp01(viewPos.x);

            Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
            transform.position = worldPos;
        }
        else
        {
            transform.position = new Vector2(0, UpPosition.transform.position.y + 3);
        }
    }
}
