using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderC : MonoBehaviour
{
    public Slider emotion;
    float emotionNum = 6;
    public static float casual_per;
    void Start()
    {
        casual_per = GameObject.FindWithTag("MainData").GetComponent<MainData>().casual_per;
        emotion = GetComponent<Slider>();
        emotion.value = 0;
        emotionNum = casual_per;
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
