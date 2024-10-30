using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startexp1 : MonoBehaviour
{
    float emotion_num = 7; 
    float currentTime;
	float startingTime = 15;
    public static string MyEmotion;
    // Start is called before the first frame update
    void Start()
    {
        MyEmotion = GameObject.FindWithTag("MainData").GetComponent<MainData>().main_emo;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        //IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
        //byte[] data = client.Receive(ref anyIP); //4
        //string text = Encoding.UTF8.GetString(data); //5
        //num = Convert.ToDouble(text);

        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0 && MyEmotion == "Natural")
        {
            SceneManager.LoadScene("Natural_Scene");
        }
        else if (currentTime <= 0 && MyEmotion == "Elegant")
        {
            SceneManager.LoadScene("Elegant_Scene");
        }
        else if(currentTime <= 0 && MyEmotion == "Erotic")
        {
            SceneManager.LoadScene("Gorgeous_Scene");
        }
        else if(currentTime <= 0 && MyEmotion == "Romantic")
        {
            SceneManager.LoadScene("Romantic_Scene");
        }
        else if(currentTime <= 0 && MyEmotion == "Casual")
        {
            SceneManager.LoadScene("Casual_Scene");
        }
        else if(currentTime <= 0 && MyEmotion == "Dynamic")
        {
            SceneManager.LoadScene("Dynamic_Scene");
        }
    }
}
