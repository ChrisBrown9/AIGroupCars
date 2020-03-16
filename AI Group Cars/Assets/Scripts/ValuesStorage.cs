
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ValuesStorage : MonoBehaviour
{
    //first layer values needed in order to fire
    //timer used for short delay on AI cars before race starts
    public float startdelay = 1.5f;

    //Front left
    //arrays intended to store values given by raycasts that AI cars use to see
    public float[] frontLeftWeights = new float[2];
    //Front right
    public float[] frontRightWeights = new float[2];
    //left
    public float[] leftWeights = new float[2];
    //right
    public float[] rightWeights = new float[2];

    //hidden layer totals
    //values for hidden layer of neural net
    //inputs get passed through weights and put here
    public float[] hiddenLayerValues = new float[2];

    //Left threat
    //weighting of lines between hidden layer and output layer
    public float[] leftThreatWeights = new float[2];
    //Right threat
    public float[] rightThreatWeights = new float[2];

    //output layer totals
    //based on the value at the end of update the car will decide what action to take 
    public float[] outputLayerValues = new float[2];

    private void Awake()
    {
        //resets all values to 0.5 
        FreshStartValues();
    }

    //resets values on awake
    public void FreshStartValues()
    {
        for (int i = 0; i < 2; i++)
        {
            frontLeftWeights[i] = 0.5f;
            frontRightWeights[i] = 0.5f;
            leftWeights[i] = 0.5f;
            rightWeights[i] = 0.5f;
        }

        for (int i = 0; i < 2; i++)
        {
            leftThreatWeights[i] = 0.5f;
            rightThreatWeights[i] = 0.5f;
        }
    }

    //takes in values from best car and second best car
    //then for each individual node or value we take randomly from mom or dad (50/50),
    //and then after we've taken the values we decide whether or not to mutate 
    public void RandomizeValues(ValuesStorage mom, ValuesStorage dad, float value)
    {
        //0 or 1, if 1 we mutate
        int yesMutate;
        //randomize before each decision in order to determine if its the father or mother 
        int parent;

        for (int i = 0; i < 2; i++)
        {
            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 2);

            if (parent == 0)
            {
                frontLeftWeights[i] = mom.frontLeftWeights[i];
            }

            else
            {
                frontLeftWeights[i] = dad.frontLeftWeights[i];
            }

            if (yesMutate == 1)
            {
                frontLeftWeights[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 2);

            if (parent == 0)
            {
                frontRightWeights[i] = mom.frontRightWeights[i];
            }

            else
            {
                frontRightWeights[i] = dad.frontRightWeights[i];
            }

            if (yesMutate == 1)
            {
                frontRightWeights[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 2);

            if (parent == 0)
            {
                leftWeights[i] = mom.leftWeights[i];
            }

            else
            {
                leftWeights[i] = dad.leftWeights[i];
            }

            if (yesMutate == 1)
            {
                leftWeights[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 2);

            if (parent == 0)
            {
                rightWeights[i] = mom.rightWeights[i];
            }

            else
            {
                rightWeights[i] = dad.rightWeights[i];
            }

            if (yesMutate == 1)
            {
                rightWeights[i] += Random.Range(-value, value);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 2);

            if (parent == 0)
            {
                leftThreatWeights[i] = mom.leftThreatWeights[i];
            }

            else
            {
                leftThreatWeights[i] = dad.leftThreatWeights[i];
            }

            if (yesMutate == 1)
            {
                leftThreatWeights[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 2);

            if (parent == 0)
            {
                rightThreatWeights[i] = mom.rightThreatWeights[i];
            }

            else
            {
                rightThreatWeights[i] = dad.rightThreatWeights[i];
            }

            if (yesMutate == 1)
            {
                rightThreatWeights[i] += Random.Range(-value, value);
            }
        }
    }

    private void FixedUpdate()
    {
        //delays start
        startdelay -= Time.deltaTime;
        if (startdelay < 0)
        {
            //reset the values in the nodes to 0 every frame
            for (int i = 0; i < 2; i++)
            {
                hiddenLayerValues[i] = 0;
                outputLayerValues[i] = 0;
            }

            //assigns input values of neural net to be the values the AI cars sense
            AICarSensors inputs = gameObject.GetComponent<AICarSensors>();

            // Check the first layer for nodes that fired, then perform additions if they did
            for (int i = 0; i < 2; i++)
            {
                hiddenLayerValues[i] += frontLeftWeights[i] * inputs.FrontLeftDist();
                hiddenLayerValues[i] += frontRightWeights[i] * inputs.FrontRightDist();
                hiddenLayerValues[i] += leftWeights[i] * inputs.LeftDist();
                hiddenLayerValues[i] += rightWeights[i] * inputs.RightDist();
            }

            for (int i = 0; i < 2; i++)
            {
                outputLayerValues[i] += leftThreatWeights[i] * hiddenLayerValues[0];
                outputLayerValues[i] += rightThreatWeights[i] * hiddenLayerValues[1];
            }

            //initializes driving actions 
            AIDrivingActions controls = GetComponent<AIDrivingActions>();

            float bestvalue = outputLayerValues[0];
            int bestvaluenum = 0;

            //if AI car wants to turn left, turn left
            //if AI car wants to turn right, turn right 
            if (outputLayerValues[1] > bestvalue)
            {
                bestvaluenum = 1;
            }

            switch (bestvaluenum)
            {
                case 0:
                    controls.TurnLeft();
                    break;
                case 1:
                    controls.TurnRight();
                    break;
            }
            //End of the AI performing its Job
        }
    }

    //replace all of the current values in the NeuralNet with the newer shinier values that may or may not improve the racing
    //abilities of the car
    //used to replicate an AI car 
    public void replaceValues(ValuesStorage newValues)
    {

        newValues.frontLeftWeights.CopyTo(frontLeftWeights, 0);
        newValues.frontRightWeights.CopyTo(frontRightWeights, 0);
        newValues.leftWeights.CopyTo(leftWeights, 0);
        newValues.rightWeights.CopyTo(rightWeights, 0);


        for (int i = 0; i < 2; i++)
        {
            hiddenLayerValues[i] = 0;
        }


        newValues.leftThreatWeights.CopyTo(leftThreatWeights, 0);
        newValues.rightThreatWeights.CopyTo(rightThreatWeights, 0);

        for (int i = 0; i < 2; i++)
        {
            outputLayerValues[i] = 0;
        }
    }

    //saves weights into text file 
    public void saveWeights()
    {
        string path = "Assets/Weights.txt";
        File.WriteAllText(path, string.Empty);
        StreamWriter write = new StreamWriter(path, true);
        write.WriteLine(frontLeftWeights[0] + " " + frontLeftWeights[1]);
        write.WriteLine(frontRightWeights[0] + " " + frontRightWeights[1]);
        write.WriteLine(leftWeights[0] + " " + leftWeights[1]);
        write.WriteLine(rightWeights[0] + " " + rightWeights[1]);
        write.WriteLine(leftThreatWeights[0] + " " + leftThreatWeights[1]);
        write.WriteLine(rightThreatWeights[0] + " " + rightThreatWeights[1]);

        write.Close();
    }

    //loads stored weights from text file into mother car 
    public void loadWeights()
    {

        List<string> readData = new List<string>();
        string[][] indVal = new string[6][];
        string path = "Assets/Weights.txt";
        StreamReader read = new StreamReader(path, true);
        while (!read.EndOfStream)
        {
            readData.Add(read.ReadLine());
        }
        for (int i = 0; i < readData.Count; i++)
        {
            string[] lineVal = readData[i].Split(' ');
            indVal[i] = new string[] { lineVal[0], lineVal[1] };
        }
        //Lemme comment pls
        frontLeftWeights[0] = float.Parse(indVal[0][0]);
        frontLeftWeights[1] = float.Parse(indVal[0][1]);

        frontRightWeights[0] = float.Parse(indVal[1][0]);
        frontRightWeights[1] = float.Parse(indVal[1][1]);

        leftWeights[0] = float.Parse(indVal[2][0]);
        leftWeights[1] = float.Parse(indVal[2][1]);

        rightWeights[0] = float.Parse(indVal[3][0]);
        rightWeights[1] = float.Parse(indVal[3][1]);

        leftThreatWeights[0] = float.Parse(indVal[4][0]);
        leftThreatWeights[1] = float.Parse(indVal[4][1]);

        rightThreatWeights[0] = float.Parse(indVal[5][0]);
        rightThreatWeights[1] = float.Parse(indVal[5][1]);

        read.Close();

        //print(readData);
    }

    public void resetStartDelay()
    {
        startdelay = 1.5f;
    }
}
