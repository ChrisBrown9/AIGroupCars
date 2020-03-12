﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarSensors : MonoBehaviour
{
    RaycastHit FrontLeft;
    RaycastHit FrontRight;
    RaycastHit FrontLeft2;
    RaycastHit FrontRight2;

    //RaycastHit Left;
    //RaycastHit Right;

    [SerializeField] Transform[] checkpoints = new Transform[52];

    int bestCheckpoint = -1;
    float distanceToNextCheckpoint;

    public bool wallCollision = false;

    // Makes all the lines that come out from the car, in order to let the car see where its going
    void FixedUpdate()
    {
        distanceToNextCheckpoint = (transform.position - checkpoints[bestCheckpoint + 1].position).magnitude;

        if (wallCollision)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward - transform.right).normalized, out FrontLeft, 50);
        Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontLeft.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward + transform.right).normalized, out FrontRight, 50);
        Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontRight.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.right).normalized, out FrontLeft2, 50);
        Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontLeft2.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 + transform.right).normalized, out FrontRight2, 50);
        Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontRight2.point);

        //Physics.Raycast(transform.position - transform.right * 0.5f, -transform.right, out Left, 50);
        //Debug.DrawLine(transform.position - transform.right * 0.5f, Left.point);

        //Physics.Raycast(transform.position + transform.right * 0.5f, transform.right, out Right, 50);
        //Debug.DrawLine(transform.position + transform.right * 0.5f, Right.point);
    }


    public float FrontLeftDist()
    {
        return FrontLeft.distance / 50f;
    }

    public float FrontRightDist()
    {
        return FrontRight.distance / 50f;
    }

    public float FrontLeft2Dist()
    {
        return FrontLeft2.distance / 50f;
    }

    public float FrontRight2Dist()
    {
        return FrontRight2.distance / 50f;
    }

    //public float LeftDist()
    //{
    //    return Left.distance / 50f;
    //}

    //public float RightDist()
    //{
    //    return Right.distance / 50f;
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Checkpoint>() != null)
        {
            int i = other.GetComponent<Checkpoint>().getCheckPointNum();

            if (i > bestCheckpoint)
            {
                bestCheckpoint = i;
                distanceToNextCheckpoint = 800;
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
    }

    public float getDistanceToNextCheckpoint()
    {
        return distanceToNextCheckpoint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && !wallCollision)
        {
            wallCollision = true;
            RaceManager.deadCars++;
        }
    }
}
