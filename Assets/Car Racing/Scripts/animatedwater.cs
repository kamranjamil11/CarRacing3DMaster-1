using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatedwater : MonoBehaviour
{
    public float speedX = 0.1f;
    public float speedy = 0.1f;
    private float curX;
    private float cury;
    // Start is called before the first frame update
    void Start()
    {
        curX = GetComponent<Renderer>().material.mainTextureOffset.x;
        cury= GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        curX += Time.deltaTime * speedX;
        cury += Time.deltaTime * speedy;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(curX, cury));

    }
}
