using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_change_photo : MonoBehaviour
{
    Renderer matColor;
    // Color_change color;
    public GameObject game_object;

    Color color;
    string hexCode;
    string mainhex = "#c2448d";
    string sub1hex = "#f59701";
    string sub2hex = "#f5a6a2";
    string sub3hex = "#f3d900";


    void Start()
    {
        matColor = game_object.GetComponent<Renderer>();

        hexCode = mainhex;

        if (gameObject.tag == "sub1_sphere")
        {
            hexCode = sub1hex;
        }
        else if (gameObject.tag == "sub2_sphere")
        {
            hexCode = sub2hex;
        }
        else if (gameObject.tag == "bottom")
        {
            hexCode = sub3hex;
        }



        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            // print(color);
            matColor.material.color = color;
            //matColor.material.color = new Color(r_color / 255f, g_color / 255f, b_color / 255f);
            //Debug.Log(true);
        }

    }

    void Update()
    {


    }
}

