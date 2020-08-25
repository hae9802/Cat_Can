using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloudCtrl : MonoBehaviour
{
    public Sprite[] backImages = new Sprite[9];
    public Sprite[] clouds = new Sprite[5];


    private BackGroundCtrl Bctrl;
    private GameCtrl Gctrl;

    private new Renderer renderer;

    private float speed;
    private float offsetX;
    

    void Start()
    {
        Gctrl = GameObject.Find("GameCtrl").GetComponent<GameCtrl>();
        Bctrl = GameObject.Find("BackGroundCtrl").GetComponent<BackGroundCtrl>();
        renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "BackGround";
        speed = Random.Range(0.05f, 0.1f);
    }

    void Update()
    {
        if(gameObject.transform.position.y < Gctrl.Up.transform.position.y - 10)
        {
            Bctrl.isCloudOff = true;
            Destroy(gameObject);
        }

        if (Bctrl.BackImage.GetComponent<SpriteRenderer>().sprite == backImages.ElementAt(2))
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", clouds.ElementAt(1).texture);
        }
        else if (Bctrl.BackImage.GetComponent<SpriteRenderer>().sprite == backImages.ElementAt(4))
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", clouds.ElementAt(2).texture);
        }
        else if (Bctrl.BackImage.GetComponent<SpriteRenderer>().sprite == backImages.ElementAt(6))
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", clouds.ElementAt(3).texture);
        }
        else if (Bctrl.BackImage.GetComponent<SpriteRenderer>().sprite == backImages.ElementAt(8))
        {
            gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", clouds.ElementAt(4).texture);
        }

        offsetX = Time.time * speed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, 0));
    }
}
