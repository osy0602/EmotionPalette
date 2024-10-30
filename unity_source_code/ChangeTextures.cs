using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeTextures : MonoBehaviour
{
    public Texture[] textures;

    public int currentTexture;
    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentTexture++;
            currentTexture %= textures.Length;

            Renderer rend = GetComponent<Renderer>();
            rend.material.mainTexture = textures[currentTexture];
        }
    }
}