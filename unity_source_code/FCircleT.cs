using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FCircleT : MonoBehaviour
{
    //0 : dynamic_per 1 :elegant_per 2:  romantic_per 3 : casual_per 4 : erotic_per 5 : natural_per
    
    float[] emotionarr;
    string main_emo;
    string sub1_emo;
    string sub2_emo;
    float main_emo_perc;
    float sub1_emo_perc;
    float sub2_emo_perc;
    private void Awake()
    {
        //RectTransform = GetComponent<RectTransform>();
   
    }
    private void Start()
    {
        emotionarr[0] = GameObject.FindWithTag("MainData").GetComponent<MainData>().dynamic_per;
        emotionarr[1] = GameObject.FindWithTag("MainData").GetComponent<MainData>().elegant_per;
        emotionarr[2] = GameObject.FindWithTag("MainData").GetComponent<MainData>().romantic_per;
        emotionarr[3] = GameObject.FindWithTag("MainData").GetComponent<MainData>().casual_per;
        emotionarr[4] = GameObject.FindWithTag("MainData").GetComponent<MainData>().erotic_per;
        emotionarr[5] = GameObject.FindWithTag("MainData").GetComponent<MainData>().natural_per;

        main_emo = GameObject.FindWithTag("MainData").GetComponent<MainData>().main_emo;
        sub1_emo = GameObject.FindWithTag("MainData").GetComponent<MainData>().sub1_emo;
        sub2_emo = GameObject.FindWithTag("MainData").GetComponent<MainData>().sub2_emo;


    }

    private void Update()
    {
        print(transform.position);
    }
}
