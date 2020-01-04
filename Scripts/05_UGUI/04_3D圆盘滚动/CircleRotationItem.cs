using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CircleRotationItem : MonoBehaviour,IPointerClickHandler
{
    private Image _itemImage;
    private Image ItemImage
    {
        get
        {
            if (_itemImage == null)
                _itemImage = GetComponent<Image>();
            return _itemImage;
        }
    }

    [NonSerialized]
    public int itemId;    //唯一不可变
    [NonSerialized]
    public int posId;     //记录当前所在位置id
    [NonSerialized]
    public CircleRotationItem before;
    [NonSerialized]
    public CircleRotationItem next;
    [NonSerialized]
    public PointData pointData;

    private CircleRotation2D control;

    public void Init(int itemId,int posId,CircleRotationItem before,CircleRotationItem next,CircleRotation2D control)
    {
        this.itemId = itemId;
        this.posId = posId;
        this.before = before;
        this.next = next;
        this.control = control;
    }

    public void SetParent(Transform parentTrans)
    {
        transform.SetParent(parentTrans, false);
    }

    //这里调用时期不确定，有可能早于Start，当在Start中给Image赋值，可能会带来问题
    public void SetImage(Sprite sp)
    {
        ItemImage.sprite = sp;
    }

    public void SetPosContinue(PointData data)      //动画切换，有过程
    {
        this.pointData = data;

        transform.DOLocalMove(new Vector3(data.point_x, data.point_y, transform.localPosition.z), 0.3f);
        transform.DOScale(Vector3.one * data.point_scale,0.3f);
    }

    public void SetPosImmediately(PointData data)    //立刻切换，用在拖拽
    {
        this.pointData = data;

        transform.localPosition = new Vector3(data.point_x, data.point_y, transform.localPosition.z);
        transform.localScale = Vector3.one * data.point_scale;
    }

    public void SetSibling(int index)
    {
        transform.SetSiblingIndex(index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        control.ClickMove(this);
    }

}
