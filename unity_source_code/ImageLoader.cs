using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ImageLoader : MonoBehaviour
{
    public Renderer thisRenderer;
    
    void Start()
    {
        StartCoroutine(GetTexture());
        //thisRenderer.material.color = Color.red;
    }
    void Update()
    {
        
    }

    IEnumerator GetTexture()
    {
        string personal_id = GameObject.FindWithTag("MainData").GetComponent<MainData>().personal_id;
        float photoCount=0;
        if (gameObject.tag == "up_left")
        {
            photoCount = 1;
        }
        else if (gameObject.tag == "up_right")
        {
            photoCount = 2;
        }
        else if (gameObject.tag == "down_left")
        {
            photoCount = 3;
        }
        else if (gameObject.tag == "down_right")
        {
            photoCount = 4;
        }
        string screenshotname;
        screenshotname = personal_id + photoCount + ".png";
        //UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://i.imgur.com/E7vJUu4.jpg");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(Application.dataPath + "/screenshot/" + screenshotname);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            thisRenderer.material.mainTexture = myTexture;
        }
        string photo_collage_name;
        photo_collage_name = personal_id + ".png";
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/photo_collage/" + photo_collage_name);
        Debug.Log(Application.dataPath);
    }
}