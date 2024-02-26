using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform mario;
    public float bound;

    // Update is called once per frame
    void Update()
    {
        if (mario.position.x - transform.position.x > bound)
        {
            transform.position =new Vector3(mario.position.x - bound,transform.position.y,transform.position.z);
        }
    }
}
