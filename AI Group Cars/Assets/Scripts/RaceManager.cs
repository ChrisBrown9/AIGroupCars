using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    [SerializeField] Transform spawnLocation;
    [SerializeField] Material challengingCars;

    [SerializeField] GameObject[] CarsList = new GameObject[20];

    [SerializeField] ValuesStorage mommy;
    [SerializeField] ValuesStorage daddy;

    public float gameTimer = 3;
    int raceNumber = 0;

    int mommyCheckpoint = -1;
    float mommyLeastDistance = 800;

    int daddyCheckpoint = -1;
    float daddyLeastDistance = 800;

    float randomizationValue = 1.1f;

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
    void Update()
    {
        gameTimer -= Time.deltaTime;
        //spawn car if gametimer is less than zero
        if (gameTimer < 0 || deadCars >= 20)
        {
            raceNumber++;
            deadCars = 0;

            findBestCars();

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
            CarsList[0].GetComponent<ValuesStorage>().replaceValues(mommy);

            for (int i = 1; i < 20; i++)
            {
                //randomize the AI for the rest of the cars
                CarsList[i].GetComponent<ValuesStorage>().replaceValues(mommy);
                //CarsList[i].GetComponent<ValuesStorage>().RandomizeValues(mommy, daddy, randomizationValue);
            }

            //print("Race Number : " + raceNumber);
            if (raceNumber < 100)
            {
                randomizationValue -= 0.01f;
            }
            if (raceNumber < 300)
            {
                //gameTimer = 5 + raceNumber / 10;
                gameTimer = 60;
                
            }
            else
            {
                gameTimer = 60;
            }
        }
    }

    void findBestCars()
    {
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
}
