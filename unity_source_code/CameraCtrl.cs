using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCtrl : MonoBehaviour
{
    int currentCamIndex = 0;
    WebCamTexture tex;
    public RawImage display;
    public Text startStopText;


    void Start()
    {

    }
    void Update()
    {
        if (tex != null)
        //stop the camera
        {
            startStopText.text = "Start Camera";
        }
        else
        //start the camera
        {

            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;

            tex.Play();
            startStopText.text = "Stop Camera";
        }
    }

}
/*
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCtrl : MonoBehaviour
{
    int currentCamIndex = 0;
    WebCamTexture tex;
    public RawImage display;
    public Text startStopText;




    public void SwapCam_Clicked() {

        if (WebCamTexture.devices.Length > 0) {

            currentCamIndex += 1;
            currentCamIndex %= WebCamTexture.devices.Length;
            //if tex is not null -> stop the webcam 
            //otherwise-> start the webcam
            if (tex != null) 
            {
                StopWebCam();
                StartStopCam_Clicked();
            }
        }
    }

    public void StartStopCam_Clicked()
    {
        if (tex != null)
        //stop the camera
        {
            StopWebCam();
            startStopText.text = "Start Camera";
        }
        else
        //start the camera
        {
    
            WebCamDevice device = WebCamTexture.devices[currentCamIndex];
            tex = new WebCamTexture(device.name);
            display.texture = tex;

            tex.Play();
            startStopText.text = "Stop Camera";
        }
        
    }

    private void StopWebCam()
    {
        display.texture = null;
        tex.Stop();
        tex = null;
    }
}
*/
