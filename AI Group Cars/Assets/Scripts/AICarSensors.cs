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

    [SerializeField] Transform[] checkpoints = new Transform[51];

    int bestCheckpoint = -1;
    float distanceToNextCheckpoint;
    float deathTimer = 3.0f; 

    public bool wallCollision = false;

    // Makes all the lines that come out from the car, in order to let the car see where its going
    void Update()
    {
        distanceToNextCheckpoint = (transform.position - checkpoints[bestCheckpoint + 1].position).magnitude;

        if (wallCollision)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        Physics.Raycast(transform.position + transform.forward * 1.3f, transform.forward, out Forward, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, Forward.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.right).normalized, out FrontLeft, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontLeft.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 + transform.right).normalized, out FrontRight, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontRight.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.up).normalized, out FrontDown, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontDown.point);

        Physics.Raycast(transform.position - transform.right * 0.5f, -transform.right, out Left, 50);
        //Debug.DrawLine(transform.position - transform.right * 0.5f, Left.point);

        Physics.Raycast(transform.position + transform.right * 0.5f, transform.right, out Right, 50);
        //Debug.DrawLine(transform.position + transform.right * 0.5f, Right.point);

        Physics.Raycast(transform.position - transform.forward * 1.3f, (-transform.forward * 2 - transform.right).normalized, out BackLeft, 50);
        //Debug.DrawLine(transform.position - transform.forward * 1.3f, BackLeft.point);

        Physics.Raycast(transform.position - transform.forward * 1.3f, (-transform.forward * 2 + transform.right).normalized, out BackRight, 50);
        //Debug.DrawLine(transform.position - transform.forward * 1.3f, BackRight.point);
    }


    public float ForwardDist()
    {
        return Forward.distance / 50f;
    }

    public float FrontLeftDist()
    {
        return FrontLeft.distance / 50f;
    }

    public float FrontRightDist()
    {
        return FrontRight.distance / 50f;
    }

    public float FrontDownDist()
    {
        return FrontDown.distance / 50f;
    }

    public float LeftDist()
    {
        return Left.distance / 50f;
    }

    public float RightDist()
    {
        return Right.distance / 50f;
    }

    public float BackLeftDist()
    {
        return BackLeft.distance / 50f;
    }

    public float BackRightDist()
    {
        return BackRight.distance / 50f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Checkpoint>() != null)
        {
            int i = other.GetComponent<Checkpoint>().getCheckPointNum();

            if (i > bestCheckpoint)
            {
                bestCheckpoint = i;
            }
        }
    }

    public int getBestCheckpoint()
    {
        return bestCheckpoint;
    }

    public void resetBestCheckpoint()
    {
        bestCheckpoint = -1;
        wallCollision = false;
        deathTimer = 3.0f;
    }

    public float getDistanceToNextCheckpoint()
    {
        return distanceToNextCheckpoint;
    }

    private void die()
    {
        wallCollision = true;
        RaceManager.deadCars++;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            die();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!wallCollision)
        {
            if (other.gameObject.tag == "MainCamera")
            {
                deathTimer -= Time.deltaTime;
                if (deathTimer < 0.0f)
                {
                    die();
                }
            }
        }
    }
}
