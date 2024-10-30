using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Text tex;
    Color col;
    float currentTime;
    float startingTime = 3;
    void Start()
    {
        currentTime = startingTime;

        col = tex.color;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if (currentTime < 0)
        {
            col.a -= Time.deltaTime;
            tex.color = col;
        }

    }
}