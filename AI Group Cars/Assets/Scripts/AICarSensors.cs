using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarSensors : MonoBehaviour
{
    //RaycastHit Forward;
    RaycastHit FrontLeft;
    RaycastHit FrontRight;
   // RaycastHit FrontDown;
    RaycastHit Left;
    RaycastHit Right;
    //RaycastHit BackLeft;
    //RaycastHit BackRight;

    //storing 51 checkpoints in array
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

        //Physics.Raycast(transform.position + transform.forward * 1.3f, transform.forward, out Forward, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, Forward.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.right).normalized, out FrontLeft, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontLeft.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 + transform.right).normalized, out FrontRight, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontRight.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 1 - transform.right).normalized, out Left, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontLeft.point);

        Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 1 + transform.right).normalized, out Right, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontRight.point);

        //Physics.Raycast(transform.position + transform.forward * 1.3f, (transform.forward * 2 - transform.up).normalized, out FrontDown, 50);
        //Debug.DrawLine(transform.position + transform.forward * 1.3f, FrontDown.point);



        //Physics.Raycast(transform.position - transform.right * 0.5f, -transform.right, out Left, 50);
        //Debug.DrawLine(transform.position - transform.right * 0.5f, Left.point);

        //Physics.Raycast(transform.position + transform.right * 0.5f, transform.right, out Right, 50);
        //Debug.DrawLine(transform.position + transform.right * 0.5f, Right.point);



        //Physics.Raycast(transform.position - transform.forward * 1.3f, (-transform.forward * 2 - transform.right).normalized, out BackLeft, 50);
        //Debug.DrawLine(transform.position - transform.forward * 1.3f, BackLeft.point);

        //Physics.Raycast(transform.position - transform.forward * 1.3f, (-transform.forward * 2 + transform.right).normalized, out BackRight, 50);
        //Debug.DrawLine(transform.position - transform.forward * 1.3f, BackRight.point);
    }

    //takes distance of front left sensor and modifies to be between value of 0 and 1
    public float FrontLeftDist()
    {
        return FrontLeft.distance / 50f;
    }


    //takes distance of front right sensor and modifies to be between value of 0 and 1
    public float FrontRightDist()
    {
        return FrontRight.distance / 50f;
    }

    //takes distance of left sensor and modifies to be between value of 0 and 1
    public float LeftDist()
    {
        return Left.distance / 50f;
    }

    //takes distance of right sensor and modifies to be between value of 0 and 1
    public float RightDist()
    {
        return Right.distance / 50f;
    }

    //on collision with a checkpoint, check if its better than your best checkpoint
    //if yes, replace best cehckpoint with it 
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

    //gets value of best checkpoint
    public int getBestCheckpoint()
    {
        return bestCheckpoint;
    }

    //resets avlues for new race 
    public void resetBestCheckpoint()
    {
        bestCheckpoint = -1;
        wallCollision = false;
        deathTimer = 3.0f;
    }
    //finds how far car is from next checkpoint list 
    public float getDistanceToNextCheckpoint()
    {
        return distanceToNextCheckpoint;
    }

    //kills car and tells racemanager that car died 
    private void die()
    {
        wallCollision = true;
        RaceManager.deadCars++;
    }

    //makes sure dead car does not collide with wall
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" && !wallCollision)
        {
            die();
        }
    }
}
