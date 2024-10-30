using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputField : MonoBehaviour
{
    public InputField InputText;
    public string InputId;
    public void Test(Text text)
    {
        text.text = InputText.text;
        InputId = text.text;
    }
}
