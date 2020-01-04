using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AUITool //: MonoBehaviour
{
    #region 单例模式
    private static AUITool instance;
    public static AUITool Instance
    {
        get
        {
            if (instance == null)
                instance= new AUITool();
            return instance;
        }
    }
    #endregion

    #region UI渗透
    /// <summary>
    /// UI渗透
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">事件数据</param>
    /// <param name="call">响应事件的类型</param>
    public void ExecuteAll<T>(PointerEventData data, ExecuteEvents.EventFunction<T> call) where T : IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);                     //获取UI事件的响应结果
        GameObject current = data.pointerCurrentRaycast.gameObject;        //*获取当前的点击UI
        foreach (var result in results)
        {
            if (result.gameObject != current)
            {
                ExecuteEvents.Execute<T>(result.gameObject, data, call);   //手动调用
            }
        }
    }
    #endregion

    #region 判断是否点击到UI
    /// <summary>
    /// 判断是否点击到UI
    /// </summary>
    /// <param name="raycaster">响应图形点击的射线检测，Canvas上一般都挂载了</param>
    /// <returns></returns>
    public bool IsClickUI(GraphicRaycaster raycaster)
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        data.pressPosition = Input.mousePosition;
        data.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);
        return results.Count>0;
    }
    #endregion


    #region  不规则按钮点击(要重写Image的IsRaycastLocationValid())
    public bool ClickIrregularityButton(PolygonCollider2D polygon)
    {
        //public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        //{
        //    Vector3 point;
        //    RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, screenPoint, eventCamera, out point);
        //    return Polygon.OverlapPoint(point);
        //}
        return true;
    }
    #endregion
}
