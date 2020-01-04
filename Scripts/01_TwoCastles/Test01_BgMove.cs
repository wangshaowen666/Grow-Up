using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test01_BgMove : MonoBehaviour
{
    private void Awake()
    {
        rawImg = GetComponent<RawImage>();
    }

    private void Update()
    {
        curSpeed += mSpeed * Time.deltaTime;
        rawImg.uvRect = new Rect(curSpeed, 0, 1, 1);
    }

    [Tooltip("UV_x增量")]
    public float mSpeed;

    private RawImage rawImg;
    private float curSpeed;   
}
