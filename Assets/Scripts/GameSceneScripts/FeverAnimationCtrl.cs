using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeverAnimationCtrl : MonoBehaviour
{
    public Sprite fever1;
    public Sprite fever2;

    private new Renderer renderer;

    private float speed;
    private float offset;
    private bool isCoroutineOn;

    void Start()
    {
        isCoroutineOn = false;
        speed = 0.2f;
        renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "FeverAnim";
        renderer.sortingOrder = 5;
    }

    void Update()
    {
        if (isCoroutineOn == false)
        {
            isCoroutineOn = true;
            StartCoroutine(Animator());
        }
        offset = Time.time * speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
    }

    IEnumerator Animator()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", fever2.texture);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", fever1.texture);
        yield return new WaitForSeconds(0.5f);
        isCoroutineOn = false;
    }
}
