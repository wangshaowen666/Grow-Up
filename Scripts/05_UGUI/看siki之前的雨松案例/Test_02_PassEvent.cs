using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test_02_PassEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(name);
        PassEvent(eventData, ExecuteEvents.pointerClickHandler);
    }

    private void PassEvent<T>(PointerEventData data,ExecuteEvents.EventFunction<T> func) where T:IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);    //找到所有能传递事件的对象
        GameObject current = data.pointerCurrentRaycast.gameObject;
        for(int i=0;i<results.Count;i++)
        {
            if(results[i].gameObject!=current)
            {
                ExecuteEvents.Execute(results[i].gameObject, data, func);
                break;  //这里break是只渗透一次就跳出
            }
        }
    }
}
