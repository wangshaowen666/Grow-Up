using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveControl : MonoBehaviour, IMoveHandler
{
    public void OnMove(AxisEventData eventData)
    {
        Debug.Log("yifong");
    }
}
