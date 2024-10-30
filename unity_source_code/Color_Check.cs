using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Check: MonoBehaviour
{

    // Color_change color;
    public Image game_Image;
    Color colorTemp;
    string hexCode;
    string mainhex;
    string sub1hex;
    string sub2hex;
    string sub3hex;
    string sub4hex;


    string sub5hex;
    string sub6hex;
    string sub7hex;
    string sub8hex;
    string sub9hex;

    string dunehex = "#eeced9";






    void Start()

    {
        game_Image = GetComponent<Image>();
        mainhex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color1;
        sub1hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color2;
        sub2hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color3;
        sub3hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color4;
        sub4hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color5;
        sub5hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color6;
        sub6hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color7;
        sub7hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color8;
        sub8hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color9;
        sub9hex = GameObject.FindWithTag("MainData").GetComponent<MainData>().color10;
    }

    void Update()
    {
       


        if (game_Image.tag == "main_sphere")
        {
            hexCode = mainhex;
        }
        else if (game_Image.tag == "sub1_sphere")
        {
            hexCode = sub1hex;
        }
        else if (game_Image.tag == "sub2_sphere")
        {
            hexCode = sub2hex;
        }
        else if (game_Image.tag == "sub3_sphere")
        {
            hexCode = sub3hex;
        }
        else if (game_Image.tag == "sub4_sphere")
        {
            hexCode = sub4hex;
        }
        else if (game_Image.tag == "dune")
        {
            hexCode = dunehex;
        }

        else if (game_Image.tag == "sub5_sphere")
        {
            hexCode = sub5hex;
        }
        else if (game_Image.tag == "sub6_sphere")
        {
            hexCode = sub6hex;
        }
        else if (game_Image.tag == "sub7_sphere")
        {
            hexCode = sub7hex;
        }
        else if (game_Image.tag == "sub8_sphere")
        {
            hexCode = sub8hex;
        }
        else if (game_Image.tag == "sub9_sphere")
        {
            hexCode = sub9hex;
        }

        /*
        else if (gameObject.tag == "wall3")
        {
            hexCode = wall3hex;
        }
        else if (gameObject.tag == "wall4")
        {
            hexCode = wall4hex;
        }
        else if (gameObject.tag == "floor")
        {
            hexCode = floorhex;
        }
        */
        
        if (ColorUtility.TryParseHtmlString(hexCode, out colorTemp))
        {
            // print(color);
            game_Image.color = colorTemp;
            //matColor.material.color = new Color(r_color / 255f, g_color / 255f, b_color / 255f);
            //Debug.Log(true);
        }
    }
    private void newColors()
    {


    }
}

