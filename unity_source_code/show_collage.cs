using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class show_collage : MonoBehaviour
{
    float currentTime;
    float startingTime = 3;
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
            SceneManager.LoadScene("photo_frame_");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("photo_frame_");
        }
       /*
        currentTime -= 1 * Time.deltaTime;
        if (currentTime <= 0)
        {
            SceneManager.LoadScene("photo_frame_");
        }
       */

    }
}
