using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_control : MonoBehaviour
{

    public GameObject[] obj_array;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.obj_array.Length; i++)
        {
            this.obj_array[i].SetActive(false);
        }

        this.obj_array[4].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void on_btn_click(int num)
    {
        for (int i = 0; i < this.obj_array.Length; i++)
        {
            this.obj_array[i].SetActive(false);
        }

        this.obj_array[num].SetActive(true);
    }
}
