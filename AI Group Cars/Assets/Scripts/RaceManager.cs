using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] Material challengingCars;

    [SerializeField] GameObject[] CarsList = new GameObject[42];

    [SerializeField] ValuesStorage mommy;
    [SerializeField] ValuesStorage daddy;

    public float gameTimer = 300;
    public int raceNumber = 0;

    int mommyCheckpoint = -1;
    float mommyLeastDistance = 800;

    int daddyCheckpoint = -1;
    float daddyLeastDistance = 800;

    float randomizationValue = 0.1f;

    public static int deadCars = 0;

    // Start is called before the first frame update
    void Start()
    {
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
            deadCars = 0;

            findBestCars();

            //print("Mommy Weights: [" + mommy.frontLeftWeights[0] + "], [" + mommy.frontLeftWeights[1] + "], [" + mommy.frontRightWeights[0] + "], [" + mommy.frontRightWeights[1] + "]");

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

            //print("Race Number : " + raceNumber);
            if (raceNumber < 100)
            {
                //randomizationValue -= 0.01f;
            }
            if (raceNumber < 300)
            {
                //gameTimer = 5 + raceNumber / 10;
                gameTimer = 300;

            }
            else
            {
                gameTimer = 300;
            }
        }
    }

    void findBestCars()
    {
        //print("Daddy Weights: " + daddy.frontLeftWeights[0] + ", " + daddy.frontLeftWeights[1]);

        for (int i = 1; i < CarsList.Length; i++)
        {
            AICarSensors testCar = CarsList[i].GetComponent<AICarSensors>();

            if (testCar.getBestCheckpoint() > mommyCheckpoint)
            {
                daddy.replaceValues(mommy);
                daddyCheckpoint = mommyCheckpoint;
                daddyLeastDistance = mommyLeastDistance;

                mommy.replaceValues(testCar.GetComponent<ValuesStorage>());
                mommyCheckpoint = testCar.getBestCheckpoint();
                mommyLeastDistance = testCar.getDistanceToNextCheckpoint();

                //print("Best Car is: " + i);

                //print("Best Weights: [" + CarsList[i].GetComponent<ValuesStorage>().frontLeftWeights[0] + "], [" + CarsList[i].GetComponent<ValuesStorage>().frontLeftWeights[1] + "], [" + CarsList[i].GetComponent<ValuesStorage>().frontRightWeights[0] + "], [" + CarsList[i].GetComponent<ValuesStorage>().frontRightWeights[1] + "]");


                //print("Test car best checkpoint :" + testCar.getBestCheckpoint());
                //print("Record holder best checkpoint :" + currentlyBestCar.getBestCheckpoint());
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
                    //print("Test car best checkpoint :" + testCar.getBestCheckpoint());
                    //print("Record holder best checkpoint :" + currentlyBestCar.getBestCheckpoint());
                }
            }

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

    public Vector3 bestCarPosition()
    {
        int bestCarNum = 0;
        int currBestCheckpoint = -1;
        float currLeastDistance = 800;

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

    public void btnSaveWeights()
    {
        mommy.saveWeights();
    }

    public void btnLoadWeights()
    {
        mommy.loadWeights();
        daddy.loadWeights();
        gameTimer = 0.01f;
    }
}
