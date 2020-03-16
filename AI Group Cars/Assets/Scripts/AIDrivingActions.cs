using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDrivingActions : MonoBehaviour
{
    //returns to true if AI on ground 
    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        //car always goes forward, as long as the AI is working
        if (GetComponent<ValuesStorage>().startdelay <= 0)
        {
            Accelerate();
        }

        //if on ground apply friction to car 
        if (grounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.95f,
                GetComponent<Rigidbody>().velocity.y,
                GetComponent<Rigidbody>().velocity.z * 0.95f);
        }
    }

    //if on ground and not in wall, turn left when function is called
    public void TurnLeft()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            transform.Rotate(new Vector3(0, -240 * Time.deltaTime, 0));
        }
    }
    //if on ground and not in wall, turn right when function is called
    public void TurnRight()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            transform.Rotate(new Vector3(0, 240 * Time.deltaTime, 0));
        }
    }

    //if on ground and not in wall, go forward, called every frame
    public void Accelerate()
    {
        if (grounded && !GetComponent<AICarSensors>().wallCollision)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 40f, ForceMode.Force);
        }
    }

    //while AI on ground, tell AI it on the ground
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Track")
        {
            grounded = true;
        }
    }

    //tell the AI its not on the ground 
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Track")
        {
            grounded = false;
        }
    }
}
