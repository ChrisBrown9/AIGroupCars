using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDrivingActions : MonoBehaviour
{
    bool grounded = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (grounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.95f,
                GetComponent<Rigidbody>().velocity.y,
                GetComponent<Rigidbody>().velocity.z * 0.95f);
        }
    }

    public void TurnLeft()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            transform.Rotate(new Vector3(0, -120 * Time.deltaTime, 0));
        }
    }

    public void TurnRight()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            transform.Rotate(new Vector3(0, 120 * Time.deltaTime, 0));
        }
    }

    public void Accelerate()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Force);
        }
    }

    public void Decelerate()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward * 50f, ForceMode.Force);
        }
    }

    public void Jump()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
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
