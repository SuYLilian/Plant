using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoppingBG : MonoBehaviour
{
    #region Test
    /* public Material mat;
     public float distance;
     public int distanceRecoard;
     [Range(0f, 0.5f)]
     public float speed = 0.2f;
     public PlayerTouch playerTouch_loopingBG;

     public TextMeshProUGUI distanceRecoard_text;

     // Start is called before the first frame update
     void Start()
     {
         mat = GetComponent<Renderer>().material;
     }

     // Update is called once per frame
     void Update()
     {
         if (playerTouch_loopingBG.canDrag == true)
         {
             distance += Time.deltaTime * speed;
             mat.SetTextureOffset("_MainTex", Vector2.up * distance);

             distanceRecoard = (int)(distance * 100);
             distanceRecoard_text.text = distanceRecoard.ToString();
         }

     }*/
    #endregion

    /*Transform cam;
    Vector3 camStartPos;
    float distance;
    //GameObject background;
    Material mat;
    float backSpeed;
    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    private void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        mat = gameObject.GetComponent<Renderer>().material;

    }*/

    public float lenth, startPos;
    public GameObject cam;
    public float parallaxEffect;

    private void Start()
    {
        startPos = transform.position.y;
        lenth = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    private void Update()
    {
        float temp = (cam.transform.position.y * (1 - parallaxEffect));
        float dist = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

        if(temp>startPos+lenth)
        {
            startPos += lenth;
        }
        else if(temp<startPos-lenth)
        {
            startPos -= lenth;
        }
    }
}

