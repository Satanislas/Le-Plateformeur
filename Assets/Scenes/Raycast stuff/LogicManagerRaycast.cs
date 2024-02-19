using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LogicManagerRaycast : MonoBehaviour
{
    public float raycastLength;
    public Transform capsule;
    public GameObject parachute;
    private Rigidbody rb;
    public float dragOnParachute;
    public float landingHeight;

    private void Start()
    {
        rb = capsule.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit raycastHit;
        if (Physics.Raycast(capsule.position, Vector3.down,out raycastHit, raycastLength))
        {
            if (raycastHit.distance <= landingHeight)
            {
                if (parachute.activeSelf)
                {
                    parachute.SetActive(false);
                    rb.drag = 0;
                }
            }
            else
            {
                Debug.DrawLine(capsule.position, capsule.position + Vector3.down * raycastLength,Color.red);
                if (!parachute.activeSelf)
                {
                    parachute.SetActive(true);
                    rb.drag = dragOnParachute;
                }
            }
        }
        else
        {
            Debug.DrawLine(capsule.position, capsule.position + Vector3.down * raycastLength,Color.blue);
        }
    }
}
