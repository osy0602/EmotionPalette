using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainData_Getter : MonoBehaviour
{
    public static string MyEmotion;
    // Start is called before the first frame update
    void Start()
    {
        MyEmotion= GameObject.FindWithTag("MainData").GetComponent<MainData>().MainEmotion;
        print(MyEmotion);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
