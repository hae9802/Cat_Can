using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSideCollider : MonoBehaviour
{
    GameCtrl Gctrl;

    private void Start()
    {
        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
    }

    private void Update()
    {

    }
}
