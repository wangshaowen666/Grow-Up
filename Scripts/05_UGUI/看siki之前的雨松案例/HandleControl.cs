using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleControl :  ScrollRect{

    protected float mRadius;

    protected override void Start()
    {
        base.Start();
        mRadius=(transform as RectTransform).sizeDelta.x*0.5f;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        var contentPos = this.content.anchoredPosition;

        //  根据contentPos的坐标象限判定移动方向。以及各种发光状态

        if(contentPos.magnitude>mRadius)
        {
            contentPos = contentPos.normalized * mRadius;
            SetContentAnchoredPosition(contentPos);
        }
    }
}
