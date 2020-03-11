using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public GameObject Car;
    [SerializeField] Transform spawnLocation;
    [SerializeField] Material challengingCars;

    [SerializeField] GameObject[] CarsList = new GameObject[20];

    [SerializeField] ValuesStorage bestAI;

    public float gameTimer = 3;
    int raceNumber = 0;

    int bestCheckpoint = -1;
    float leastDistance = 800;
    float randomizationValue = 10f;

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
        if (gameTimer < 0)
        {
            raceNumber++;

            findBestCar();

            for (int i = 0; i < 20; i++)
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

            for (int i = 1; i < 20; i++)
            {
                //randomize the AI for the rest of the cars
                //CarsList[i].GetComponent<ValuesStorage>().replaceValues(CarsList[0].GetComponent<ValuesStorage>());
                //CarsList[i].GetComponent<ValuesStorage>().RandomizeValues(randomizationValue);

                CarsList[i].GetComponent<ValuesStorage>().makeBaby(bestAI, randomizationValue);
            }

            //print("Race Number : " + raceNumber);
            if (raceNumber < 500)
            {
                //randomizationValue -= 0.001f;
            }
            if (raceNumber < 300)
            {
                gameTimer = 5 + raceNumber / 10;
                
            }
            else
            {
                gameTimer = 40;
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
}
