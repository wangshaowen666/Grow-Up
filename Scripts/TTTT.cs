using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TTTT : MonoBehaviour//, IPointerClickHandler
{
    public Vector2 vec;

    List<MySt> sts;

    List<Vector3> poss;
    void Test()
    {

        sts = new List<MySt>();

        MySt mySt = new MySt();
        mySt.a = 100;
        sts.Add(mySt);

       // sts[0].b = 90;
    }
}

public struct MySt
{
    public int a;
    public int b;
}
