using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCtrl : MonoBehaviour
{

    //Vector3 target = new Vector3(0,0,0);
    public GameObject targetPosition;
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = targetPosition.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, 1f);
    }
    
}
