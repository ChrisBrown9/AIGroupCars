using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject Car;
    [SerializeField] Transform spawnLocation;
    [SerializeField] Material challengingCars;

    [SerializeField] GameObject[] CarsList = new GameObject[40];

    [SerializeField] ValuesStorage bestAI;

    public float gameTimer;
    [SerializeField] int raceNumber = 0;

    public static int deadCars = 0;

    int bestCheckpoint = -1;
    float leastDistance = 800;

    // Start is called before the first frame update
    void Start()
    {
        bestAI.FreshStartValues();

        for (int i = 1; i < 20; i++)
        {
            CarsList[i].GetComponentInChildren<MeshRenderer>().material = challengingCars;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameTimer -= Time.deltaTime;
        //spawn car if gametimer is less than zero
        if (gameTimer < 0 || deadCars >= CarsList.Length)
        {
            raceNumber++;

            findBestCar();
            deadCars = 0;

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
            CarsList[0].GetComponent<ValuesStorage>().replaceValues(bestAI);

            for (int i = 1; i < CarsList.Length; i++)
            {
                //randomize the AI for the rest of the cars
                //CarsList[i].GetComponent<ValuesStorage>().replaceValues(CarsList[0].GetComponent<ValuesStorage>());
                //CarsList[i].GetComponent<ValuesStorage>().RandomizeValues(randomizationValue);

                CarsList[i].GetComponent<ValuesStorage>().makeBaby(bestAI);
                //CarsList[i].GetComponent<ValuesStorage>().replaceValues(bestAI);
            }

            if (raceNumber < 50)
            {
                gameTimer = 400;

            }
            else
            {
                gameTimer = 400;
            }
        }
    }

    void findBestCar()
    {
        for (int i = 1; i < CarsList.Length; i++)
        {
            AICarSensors testCar = CarsList[i].GetComponent<AICarSensors>();

            if (testCar.getBestCheckpoint() > bestCheckpoint)
            {
                bestAI.replaceValues(testCar.GetComponent<ValuesStorage>());
                bestCheckpoint = testCar.getBestCheckpoint();
                leastDistance = testCar.getDistanceToNextCheckpoint();
                //print("Test car best checkpoint :" + testCar.getBestCheckpoint());
                //print("Record holder best checkpoint :" + currentlyBestCar.getBestCheckpoint());
            }
            else if (testCar.getBestCheckpoint() == bestCheckpoint)
            {
                if (testCar.getDistanceToNextCheckpoint() < leastDistance)
                {
                    bestAI.replaceValues(testCar.GetComponent<ValuesStorage>());
                    bestCheckpoint = testCar.getBestCheckpoint();
                    leastDistance = testCar.getDistanceToNextCheckpoint();
                    //print("Test car best checkpoint :" + testCar.getBestCheckpoint());
                    //print("Record holder best checkpoint :" + currentlyBestCar.getBestCheckpoint());
                }
            }
        }
    }

    public Vector3 bestCarPosition()
    {
        int bestCarNum = 0;
        int currBestCheckpoint = -1;
        float currLeastDistance = 800;

        for (int i = 1; i < CarsList.Length; i++)
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
}
