using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderR : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider emotion;
    float emotionNum = 6;
    public static float romantic_per;
    void Start()
    {
        romantic_per = GameObject.FindWithTag("MainData").GetComponent<MainData>().romantic_per;
        emotion = GetComponent<Slider>();
        emotion.value = 0;
        emotionNum = romantic_per;
    }

    // Update is called once per frame
    void Update()
    {
        if (emotion.value < emotionNum)
        {
            if (emotionNum > 10)
            {
                emotion.value += Time.deltaTime * 30;
            }
            else
                emotion.value += Time.deltaTime * 5;
        }
    }
}