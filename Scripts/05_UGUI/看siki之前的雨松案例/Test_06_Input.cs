using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Test_06_Input : MonoBehaviour {

    public InputField field;

    private void Start()
    {
        field.characterLimit = 6;
        field.contentType = InputField.ContentType.Password;
       // field.contentType = InputField.ContentType.Password;

        
        field.onEndEdit.AddListener(MyEnd);
    }

    private void MyEnd(string arg0)
    {
        Debug.Log("dd" + arg0);
    }

    //在单独聊天时用
    private char MyInput(string text, int charIndex, char addedChar)
    {
        return addedChar;
    }
}
