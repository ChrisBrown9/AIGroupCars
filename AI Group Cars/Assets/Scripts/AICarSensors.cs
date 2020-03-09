using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarSensors : MonoBehaviour
{
    RaycastHit Forward;
    RaycastHit FrontLeft;
    RaycastHit FrontRight;
    RaycastHit FrontDown;
    RaycastHit Left;
    RaycastHit Right;
    RaycastHit BackLeft;
    RaycastHit BackRight;

    // Makes all the lines that come out from the car, in order to let the car see where its going
    void Update()
    {
        
        Physics.Raycast(transform.position + transform.forward * 1.3f, transform.forward, out Forward, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, Forward.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.right).normalized, out FrontLeft, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontLeft.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 + transform.right).normalized, out FrontRight, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontRight.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.up).normalized, out FrontDown, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontDown.point);

        Physics.Raycast(transform.position - transform.forward * 1.3f, (-transform.forward * 2 - transform.right).normalized, out BackLeft, 50);
        //Debug.DrawLine(transform.position - transform.forward * 1.3f, BackLeft.point);

        Physics.Raycast(transform.position - transform.forward * 1.3f, (-transform.forward * 2 + transform.right).normalized, out BackRight, 50);
        //Debug.DrawLine(transform.position - transform.forward * 1.3f, BackRight.point);
    }


    public float ForwardDist()
    {
        return Forward.distance;
    }

    public float FrontLeftDist()
    {
        return FrontLeft.distance;
    }

    public float FrontRightDist()
    {
        return FrontRight.distance;
    }

    public float FrontDownDist()
    {
        return FrontDown.distance;
    }

    public float BackLeftDist()
    {
        return BackLeft.distance;
    }

    public float BackRightDist()
    {
        return BackRight.distance;
    }

}
