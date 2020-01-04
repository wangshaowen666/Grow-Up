using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_04_UIEffectOrder : MonoBehaviour {

    public int sortOrder;

    private void Start()
    {
        Refresh();
    }

    private Canvas _canvas;
    public Canvas canvas
    {
        get
        {
            if (_canvas == null)
                _canvas = GetComponent<Canvas>();
            if (_canvas == null)
                _canvas = gameObject.AddComponent<Canvas>();
            _canvas.hideFlags = HideFlags.NotEditable; //其代表在Inspector面板不可编辑
            return _canvas;
        }
    }

    public void Refresh()
    {
        canvas.overrideSorting = true;  //允许嵌套的画布忽略父绘制顺序和绘制顶部或下方。
        canvas.sortingOrder = sortOrder;

        foreach(var ps in GetComponentsInChildren<ParticleSystemRenderer>())
        {
            ps.sortingOrder = sortOrder;
        }
    }
}
