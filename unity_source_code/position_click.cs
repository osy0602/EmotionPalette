using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class position_click : MonoBehaviour
{

    float m_fSpeed = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        float fHorizontal = Input.GetAxis("Horizontal");
        float fVertical = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * Time.deltaTime * m_fSpeed * fHorizontal, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * m_fSpeed * fVertical, Space.World);
    }
}
