using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_01_ShowRaycast : MonoBehaviour {

#if UNITY_EDITOR
    Vector3[] corners = new Vector3[4];

    //系统周期函数，每帧执行,非运行状态下也会执行。   Gizmos 线框
    private void OnDrawGizmos()
    {
        foreach(var g in FindObjectsOfType<MaskableGraphic>())
        {
            if(g.raycastTarget)
            {
                RectTransform rect = g.transform as RectTransform;
                rect.GetWorldCorners(corners);
                Gizmos.color = Color.blue;
                for (int i = 0; i < 4; i++)
                    Gizmos.DrawLine(corners[i], corners[(i + 1)%4]);
            }
        }
    }

#endif 
}
