using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startprj2 : MonoBehaviour
{
    float currentTime;
    float startingTime = 7;
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
            SceneManager.LoadScene("exp1_intro");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("exp1_intro");
        }

    }
}
