using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDrivingActions : MonoBehaviour
{
    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.95f, 
            GetComponent<Rigidbody>().velocity.y, 
            GetComponent<Rigidbody>().velocity.z *0.95f);
    }

    public void TurnLeft()
    {
        if (grounded)
        {
            transform.Rotate(new Vector3(0, -2, 0));
        }
    }

    public void TurnRight()
    {
        if (grounded)
        {
            transform.Rotate(new Vector3(0, 2, 0));
        }
    }

    public void Accelerate()
    {
        if (grounded)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 1f, ForceMode.Impulse);
        }
    }

    public void Decelerate()
    {
        if (grounded)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * 1f, ForceMode.Impulse);
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Track")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Track")
        {
            grounded = false;
        }
    }
}
