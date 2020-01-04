using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_07_RawImage : MonoBehaviour {

    public RawImage m_RawImage;

    public float strength = 0.1f;

    float value_x = 0;
    float value_y = 0;
    private void Update()
    {
        value_x += (Input.GetAxisRaw("Horizontal")*0.01f);
        value_y += (Input.GetAxisRaw("Vertical") * 0.01f);
        value_x = Mathf.Clamp(value_x, 0, 1 - strength);
        value_y = Mathf.Clamp(value_y, 0, 1 - strength);
        m_RawImage.uvRect = new Rect(value_x, value_y, strength, strength);
    }
}
