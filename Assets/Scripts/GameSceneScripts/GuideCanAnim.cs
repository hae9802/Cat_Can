using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideCanAnim : MonoBehaviour
{
    private bool isCoIng;
    private Color tmpColor;

    private void Start()
    {
        tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        isCoIng = false;
    }

    private void Update()
    {
        if (isCoIng == false)
            StartCoroutine(AnimCo());
    }

    IEnumerator AnimCo()
    {
        isCoIng = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
        yield return new WaitForSeconds(0.2f);
        isCoIng = false;

    }
}
