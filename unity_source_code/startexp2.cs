using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class startexp2 : MonoBehaviour
{
    float currentTime;
    float startingTime = 15;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0)
        { 
            SceneManager.LoadScene("emotion_photo_ex");//포토체험 씬으로 로드하기
        }
       
    }
}
