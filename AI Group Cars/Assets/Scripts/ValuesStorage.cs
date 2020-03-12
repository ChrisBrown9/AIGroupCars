//Sigmoid function gotten from https://stackoverflow.com/questions/412019/math-optimization-in-c-sharp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesStorage : MonoBehaviour
{
    //first layer values needed in order to fire
    float startdelay = 1.5f;

    //Front left
    public float[] frontLeftWeights = new float[2];
    //Front right
    public float[] frontRightWeights = new float[2];
    //left
    public float[] leftWeights = new float[2];
    //right
    public float[] rightWeights = new float[2];

    //hidden layer totals
    public float[] hiddenLayerValues = new float[2];

    //Left threat
    public float[] leftThreatWeights = new float[2];
    //Right threat
    public float[] rightThreatWeights = new float[2];

    //output layer totals
    public float[] outputLayerValues = new float[2];

    private void Awake()
    {
        FreshStartValues();
    }

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

    public void RandomizeValues(ValuesStorage mom, ValuesStorage dad, float value)
    {
        int yesMutate;
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
        startdelay -= Time.deltaTime;
        if (startdelay < 0)
        {
            //reset the values in the nodes to 0 every frame
            for (int i = 0; i < 2; i++)
            {
                hiddenLayerValues[i] = 0;
                outputLayerValues[i] = 0;
            }
            AICarSensors inputs = gameObject.GetComponent<AICarSensors>();

            /// Check the first layer for nodes that fired, then perform additions if they did
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

            AIDrivingActions controls = GetComponent<AIDrivingActions>();

            float bestvalue = outputLayerValues[0];
            int bestvaluenum = 0;

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
            ///End of the AI performing its Job
        }
    }

    //replace all of the current values in the NeuralNet with the newer shinier values that may or may not improve the racing
    //abilities of the car
    public void replaceValues(ValuesStorage newValues)
    {
        //newValues.layer1Threshholds.CopyTo(layer1Threshholds, 0);

        newValues.frontLeftWeights.CopyTo(frontLeftWeights, 0);
        newValues.frontRightWeights.CopyTo(frontRightWeights, 0);
        newValues.leftWeights.CopyTo(leftWeights, 0);
        newValues.rightWeights.CopyTo(rightWeights, 0);

        // newValues.layer2NodeValues.CopyTo(layer2NodeValues, 0);

        for (int i = 0; i < 2; i++)
        {
            hiddenLayerValues[i] = 0;
        }

        //newValues.layer2Threshholds.CopyTo(layer2Threshholds, 0);

        newValues.leftThreatWeights.CopyTo(leftThreatWeights, 0);
        newValues.rightThreatWeights.CopyTo(rightThreatWeights, 0);

        //newValues.layer3NodeValues.CopyTo(layer3NodeValues, 0);
        for (int i = 0; i < 2; i++)
        {
            outputLayerValues[i] = 0;
        }
    }

    float sigmoid(float inValue)
    {
        return (float)1f / (1f + Mathf.Exp(inValue));
    }

    public void resetStartDelay()
    {
        startdelay = 1.5f;
    }
}
