using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class CircleRotation2D : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public CircleType circleType;   //这里我有一个需求，固定周长时Inspector面板显示周长参数，固定间隔时Inspector面板显示间隔参数 

    [Tooltip("成员的宽高")]
    public Vector2 cellsize;
    [Tooltip("圆环周长")]
    public float length;
    [Tooltip("成员间距")]
    public float offset;
    [Tooltip("圆环高度")]
    public float height;

    public float scale_min;
    public float scale_max;

    public Sprite[] sprites;

    private List<CircleRotationItem> items;
    Dictionary<int, PointData> pointDic;         //记录预设位置点的数据关系,值不会更新

    private void Start()  
    {
        items = new List<CircleRotationItem>();
        pointDic = new Dictionary<int, PointData>();
        CreatItems();
    }

    public void CreatItems()
    {
        int itemCount = sprites.Length;
        if (circleType == CircleType.FixLength)
            offset = length / itemCount - cellsize.x;
        else if (circleType == CircleType.FixOffest)
            length = (offset + cellsize.x) * itemCount;

        foreach (var sp in sprites)
        {
            GameObject go = CreatTemplate();
            CircleRotationItem item = Instantiate(go).GetComponent<CircleRotationItem>();
            item.GetComponent<RectTransform>().sizeDelta = cellsize;
            item.SetParent(transform);
            item.SetImage(sp);
            items.Add(item);

            Destroy(go);
        }

        for (int i = 0; i < items.Count; i++)
        {
            items[i].Init(i, i, items[(i - 1 + items.Count) % items.Count], items[(i + 1) % items.Count],this);
            GetPointData(i * 1.0f / items.Count,ref items[i].pointData);
            items[i].SetPosImmediately(items[i].pointData);

            pointDic.Add(i, items[i].pointData);
        }
        SetChildSibling();
    }

    private GameObject CreatTemplate()
    {
        GameObject go = new GameObject("Template");
        go.AddComponent<Image>();
        go.AddComponent<CircleRotationItem>();
        return go;
    }

    public void GetPointData(float rate,ref PointData data)
    {
        if(rate>1||rate<0)
            Debug.LogError("参数范围有误");

        rate = rate == 1 ? 0 : rate;
        if(rate<0.25f)
        {
            data.point_x = length * rate;   
            data.point_y = height * 2 * rate;
            data.point_scale = scale_max - (scale_max - scale_min) * 2 * rate;
        }
        else if(rate>=0.25f&&rate<0.5f)
        {
            data.point_x = length * (0.5f - rate);
            data.point_y = height * 2 * rate;
            data.point_scale = scale_max - (scale_max - scale_min) * 2 * rate;

        }
        else if(rate>=0.5f&&rate<0.75f)
        {
            data.point_x = length * (0.5f - rate);
            data.point_y = height * 2 * (1 - rate);
            data.point_scale = scale_min + (scale_max - scale_min) * (rate - 0.5f) * 2;
        }
        else if(rate>=0.75f&&rate<1)
        {
            data.point_x = length * (rate - 1);
            data.point_y = height * 2 * (1 - rate);
            data.point_scale = scale_min + (scale_max - scale_min) * (rate - 0.5f) * 2;
        }
    }

    public void SetChildSibling()
    {        
        var list = items.OrderByDescending(l => l.pointData.point_y).ToList<CircleRotationItem>();
        //var list = (from l in items orderby l.pointData.point_y descending select l).ToList<CircleRotationItem>();
        list.ForEach(a => { a.SetSibling(items.Count);});
    }

    public void ClickMove(CircleRotationItem clickItem)
    {
        int moveIndex = clickItem.posId <= items.Count / 2 ? -1 : 1;

        for(int i=0;i<items.Count;i++)
        {
            var posId = (items[i].posId + moveIndex + items.Count) % items.Count;  //这里+了一个item.count是避免posID为0时0-1=-1 得到负数

            items[i].posId = posId;
            items[i].SetPosContinue(pointDic[posId]);
        }
        SetChildSibling();
    }

    //拖拽  归正
    public void DragMove(float changeValue)
    {

    }

    private float dragValue;
    public void OnDrag(PointerEventData eventData)
    {
        dragValue += eventData.delta.x;

        DragMove(dragValue);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragValue = 0;
    }
}

public enum CircleType
{
    FixLength,
    FixOffest
}

public struct PointData
{
    public float point_x;
    public float point_y;
    public float point_scale;
}
