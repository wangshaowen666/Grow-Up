using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddClickMethod : MonoBehaviour{

    public Transform img1;

    private Button mBtn;

    private void Start()
    {
        AddEventTrigger(img1,EventTriggerType.PointerClick,(a)=> { Debug.Log(img1.name); });
        mBtn = GetComponent<Button>();
        mBtn.onClick.AddListener(() => { Debug.Log(mBtn.name); });
    }

   

    public void AddEventTrigger(Transform insObject, EventTriggerType eventType, UnityAction<BaseEventData> myFunction)//泛型委托
    {
        EventTrigger eventTri = insObject.GetComponent<EventTrigger>() ?? insObject.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener(myFunction);
        eventTri.triggers.Add(entry);
    }
}
