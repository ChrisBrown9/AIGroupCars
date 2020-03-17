//Group Members: Chris Brown, Aidan Fallis, Zacchary Labas, Sean Binnie

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    //location & rotation we set cars to at beginning of race
    [SerializeField] Transform spawnLocation;
    
    //stores the cars 
    [SerializeField] GameObject[] CarsList = new GameObject[42];

    //stores weights of best car
    [SerializeField] ValuesStorage mommy;
    //stores weights of second best car 
    [SerializeField] ValuesStorage daddy;

    //resets race if cars take too long to finish it 
    public float gameTimer = 300;

    //tracks the generation of cars
    public int raceNumber = 0;

    //farthest checkpoint the racecar has reached
    int mommyCheckpoint = -1;
    //distance tell next checkpoint 
    float mommyLeastDistance = 800;

    //farthest checkpoint the racecar has reached
    int daddyCheckpoint = -1;
    //distance tell next checkpoint 
    float daddyLeastDistance = 800;

    //when a car is mutated, this is the maximum deviation from the current value stored 
    float randomizationValue = 0.1f;

    //tracks crashed cars
    public static int deadCars = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        gameTimer -= Time.deltaTime;
        //spawn car if gametimer is less than zero
        if (gameTimer < 0 || deadCars >= CarsList.Length)
        {
            raceNumber++;
            deadCars = 0;

            //calls function
            findBestCars();

            for (int i = 0; i < CarsList.Length; i++)
            {
                CarsList[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                CarsList[i].GetComponent<Rigidbody>().freezeRotation = true;
                CarsList[i].transform.position = spawnLocation.position;
                CarsList[i].transform.rotation = spawnLocation.rotation;
                CarsList[i].GetComponent<AICarSensors>().resetBestCheckpoint();
                CarsList[i].GetComponent<ValuesStorage>().resetStartDelay();
            }

            //assign car 1 the currently best AI
            CarsList[0].GetComponent<ValuesStorage>().replaceValues(mommy);
            CarsList[1].GetComponent<ValuesStorage>().replaceValues(daddy);

            for (int i = 2; i < CarsList.Length; i++)
            {
                //randomize the AI for the rest of the cars
                CarsList[i].GetComponent<ValuesStorage>().replaceValues(mommy);
                CarsList[i].GetComponent<ValuesStorage>().RandomizeValues(mommy, daddy, randomizationValue);
            }

            //resets time when race resets 
            gameTimer = 300;
        }
    }

    //replaces values inside of mommy and daddy cars with the new best cars from the previous generation
    //if there are better cars 
    void findBestCars()
    {

        for (int i = 1; i < CarsList.Length; i++)
        {
            AICarSensors testCar = CarsList[i].GetComponent<AICarSensors>();

            //checks if any cars were better than mommy, if there are, replaces mommy and daddy with correct new values 
            if (testCar.getBestCheckpoint() > mommyCheckpoint)
            {
                daddy.replaceValues(mommy);
                daddyCheckpoint = mommyCheckpoint;
                daddyLeastDistance = mommyLeastDistance;

                mommy.replaceValues(testCar.GetComponent<ValuesStorage>());
                mommyCheckpoint = testCar.getBestCheckpoint();
                mommyLeastDistance = testCar.getDistanceToNextCheckpoint();
            }
            else if (testCar.getBestCheckpoint() == mommyCheckpoint)
            {
                if (testCar.getDistanceToNextCheckpoint() < mommyLeastDistance)
                {
                    daddy.replaceValues(mommy);
                    daddyCheckpoint = mommyCheckpoint;
                    daddyLeastDistance = mommyLeastDistance;

                    mommy.replaceValues(testCar.GetComponent<ValuesStorage>());
                    mommyCheckpoint = testCar.getBestCheckpoint();
                    mommyLeastDistance = testCar.getDistanceToNextCheckpoint();
                }
            }

            //checks if any cars were better than daddy, if there are, replaces daddy with correct new values 
            else if (testCar.getBestCheckpoint() > daddyCheckpoint)
            {
                daddy.replaceValues(testCar.GetComponent<ValuesStorage>());
                daddyCheckpoint = testCar.getBestCheckpoint();
                daddyLeastDistance = testCar.getDistanceToNextCheckpoint();
                //print("Test car best checkpoint :" + testCar.getBestCheckpoint());
                //print("Record holder best checkpoint :" + currentlyBestCar.getBestCheckpoint());
            }
            else if (testCar.getBestCheckpoint() == daddyCheckpoint)
            {
                if (testCar.getDistanceToNextCheckpoint() < daddyLeastDistance)
                {
                    daddy.replaceValues(testCar.GetComponent<ValuesStorage>());
                    daddyCheckpoint = testCar.getBestCheckpoint();
                    daddyLeastDistance = testCar.getDistanceToNextCheckpoint();
                    //print("Test car best checkpoint :" + testCar.getBestCheckpoint());
                    //print("Record holder best checkpoint :" + currentlyBestCar.getBestCheckpoint());
                }
            }
        }
    }

    //returns position of current leading car so we know where the camera should follow 
    public Vector3 bestCarPosition()
    {
        int bestCarNum = 0;
        int currBestCheckpoint = -1;
        float currLeastDistance = 800;

        //loops through all cars and finds current best one so camera knows who to follow 
        for (int i = 0; i < CarsList.Length; i++)
        {
            if (!CarsList[i].GetComponent<AICarSensors>().wallCollision)
            {

                AICarSensors testCar = CarsList[i].GetComponent<AICarSensors>();

                if (testCar.getBestCheckpoint() > currBestCheckpoint)
                {
                    bestCarNum = i;
                    currBestCheckpoint = testCar.getBestCheckpoint();
                    currLeastDistance = testCar.getDistanceToNextCheckpoint();
                }
                else if (testCar.getBestCheckpoint() == currBestCheckpoint)
                {
                    if (testCar.getDistanceToNextCheckpoint() < currLeastDistance)
                    {
                        bestCarNum = i;
                        currBestCheckpoint = testCar.getBestCheckpoint();
                        currLeastDistance = testCar.getDistanceToNextCheckpoint();
                    }
                }
            }
        }

        return CarsList[bestCarNum].GetComponent<Transform>().position;
    }

    //when button clicked saves weights 
    public void btnSaveWeights()
    {
        mommy.saveWeights();
    }

    //when button clicked, loads weights 
    public void btnLoadWeights()
    {
        mommy.loadWeights();
        daddy.loadWeights();
        gameTimer = 0.01f;
    }
}
