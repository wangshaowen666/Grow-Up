using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Test_05_UIGray : MonoBehaviour
{
    private bool _isGray = false;
    public bool isGray
    {
        get { return _isGray; }
        set { _isGray = value; }
    }

    private void OnGUI()
    {
        if(GUILayout.Button("BUttonA"))
        {
            Debug.Log("点击A");
        }
    }
}

