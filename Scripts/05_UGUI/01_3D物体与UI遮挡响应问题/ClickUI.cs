using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.ExecuteEvents;

public class ClickUI : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        img.color = Color.blue;
        ExecuteAll(eventData,ExecuteEvents.pointerDownHandler);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.color = Color.red;
        ExecuteAll(eventData, ExecuteEvents.pointerUpHandler);
    }

    //UI渗透
    private void ExecuteAll<T>(PointerEventData data,ExecuteEvents.EventFunction<T> call) where T:IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        foreach(var result in results)
        {
            if(result.gameObject!= current)
            {
                ExecuteEvents.Execute<T>(result.gameObject, data, call);
            }
        }
    }   
}
