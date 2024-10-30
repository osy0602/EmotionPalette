using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace EasyGameStudio.Jeremy
{
    public class Webcam_control : MonoBehaviour
    {
        public Material material;

        //camera texture
        private WebCamTexture cam_texture;



        void OnEnable()
        {
  
            StartCoroutine(this.open_camera());
        }


        public IEnumerator open_camera()
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                WebCamDevice[] devices = WebCamTexture.devices;
                string deviceName = devices[1].name;
                this.cam_texture = new WebCamTexture(deviceName);
                this.cam_texture.Play();

                this.material.SetTexture("_BaseMap", cam_texture);
            }
        }

        void OnDisable()
        {
            this.cam_texture.Stop();
            this.cam_texture = null;
            StopAllCoroutines();
        }


    }
}

