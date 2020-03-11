//Sigmoid function gotten from https://stackoverflow.com/questions/412019/math-optimization-in-c-sharp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesStorage : MonoBehaviour
{
    //first layer values needed in order to fire
    float startdelay = 1.5f;

    //not chris
    public float[] hiddenLayerWeights1 = new float[4];
    public float[] hiddenLayerWeights2 = new float[4];
    public float[] hiddenLayerWeights3 = new float[4];
    public float[] hiddenLayerWeights4 = new float[4];
    public float[] hiddenLayerWeights5 = new float[4];
    public float[] hiddenLayerWeights6 = new float[4];
    public float[] hiddenLayerWeights7 = new float[4];
    public float[] hiddenLayerWeights8 = new float[4];

    public float[] hiddenLayerValues = new float[4];

    public float[] outputLayerWeights1 = new float[5];
    public float[] outputLayerWeights2 = new float[5];
    public float[] outputLayerWeights3 = new float[5];
    public float[] outputLayerWeights4 = new float[5];

    public float[] outputLayerValues = new float[5];

    private void Awake()
    {
        FreshStartValues();
    }

    public void FreshStartValues()
    {
        /*
        for (int i = 0; i < 4; i++)
        {
            hiddenLayerWeights1[i] = 0.5f;
            hiddenLayerWeights2[i] = 0.5f;
            hiddenLayerWeights3[i] = 0.5f;
            hiddenLayerWeights4[i] = 0.5f;
            hiddenLayerWeights5[i] = 0.5f;
            hiddenLayerWeights6[i] = 0.5f;
            hiddenLayerWeights7[i] = 0.5f;
            hiddenLayerWeights8[i] = 0.5f;
        }

        for (int i = 0; i < 5; i++)
        {
            outputLayerWeights1[i] = 0.5f;
            outputLayerWeights2[i] = 0.5f;
            outputLayerWeights3[i] = 0.5f;
            outputLayerWeights4[i] = 0.5f;
        }
        */

        hiddenLayerWeights1[0] = 0.0f;
        hiddenLayerWeights2[0] = 0.5f;
        hiddenLayerWeights3[0] = 0.0f;
        hiddenLayerWeights4[0] = 0.0f;
        hiddenLayerWeights5[0] = 0.5f;
        hiddenLayerWeights6[0] = 0.0f;
        hiddenLayerWeights7[0] = 0.3f;
        hiddenLayerWeights8[0] = 0.0f;

        hiddenLayerWeights1[1] = 0.0f;
        hiddenLayerWeights2[1] = 0.0f;
        hiddenLayerWeights3[1] = 0.5f;
        hiddenLayerWeights4[1] = 0.0f;
        hiddenLayerWeights5[1] = 0.0f;
        hiddenLayerWeights6[1] = 0.5f;
        hiddenLayerWeights7[1] = 0.0f;
        hiddenLayerWeights8[1] = 0.3f;

        hiddenLayerWeights1[2] = 0.0f;
        hiddenLayerWeights2[2] = 0.0f;
        hiddenLayerWeights3[2] = 0.0f;
        hiddenLayerWeights4[2] = 0.5f;
        hiddenLayerWeights5[2] = 0.0f;
        hiddenLayerWeights6[2] = 0.0f;
        hiddenLayerWeights7[2] = 0.0f;
        hiddenLayerWeights8[2] = 0.0f;

        hiddenLayerWeights1[3] = 0.7f;
        hiddenLayerWeights2[3] = 0.7f;
        hiddenLayerWeights3[3] = 0.7f;
        hiddenLayerWeights4[3] = 0.0f;
        hiddenLayerWeights5[3] = 0.0f;
        hiddenLayerWeights6[3] = 0.0f;
        hiddenLayerWeights7[3] = 0.0f;
        hiddenLayerWeights8[3] = 0.0f;

        outputLayerWeights1[0] = 0.3f;
        outputLayerWeights2[0] = 0.5f;
        outputLayerWeights3[0] = 0.0f;
        outputLayerWeights4[0] = 0.5f;

        outputLayerWeights1[1] = 0.5f;
        outputLayerWeights2[1] = 0.3f;
        outputLayerWeights3[1] = 0.0f;
        outputLayerWeights4[1] = 0.5f;

        outputLayerWeights1[2] = 0.5f;
        outputLayerWeights2[2] = 0.5f;
        outputLayerWeights3[2] = 0.0f;
        outputLayerWeights4[2] = 0.5f;

        outputLayerWeights1[3] = 0.0f;
        outputLayerWeights2[3] = 0.0f;
        outputLayerWeights3[3] = 0.0f;
        outputLayerWeights4[3] = 0.0f;

        outputLayerWeights1[4] = 0.0f;
        outputLayerWeights2[4] = 0.0f;
        outputLayerWeights3[4] = 0.5f;
        outputLayerWeights4[4] = 0.0f;
    }

    public void RandomizeValues(ValuesStorage mom, ValuesStorage dad, float value)
    {
        int yesMutate;
        int parent;

        print(mom.hiddenLayerValues[0] + ", " + mom.hiddenLayerValues[1] + ", " + mom.hiddenLayerValues[2] + ", " + mom.hiddenLayerValues[3]);
        print(dad.hiddenLayerValues[0] + ", " + dad.hiddenLayerValues[1] + ", " + dad.hiddenLayerValues[2] + ", " + dad.hiddenLayerValues[3]);

        for (int i = 0; i < 4; i++)
        {
            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights1[i] = mom.hiddenLayerWeights1[i];
            }

            else
            {
                hiddenLayerWeights1[i] = dad.hiddenLayerWeights1[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights1[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights2[i] = mom.hiddenLayerWeights2[i];
            }

            else
            {
                hiddenLayerWeights2[i] = dad.hiddenLayerWeights2[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights2[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights3[i] = mom.hiddenLayerWeights3[i];
            }

            else
            {
                hiddenLayerWeights3[i] = dad.hiddenLayerWeights3[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights3[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights4[i] = mom.hiddenLayerWeights4[i];
            }

            else
            {
                hiddenLayerWeights4[i] = dad.hiddenLayerWeights4[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights4[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights5[i] = mom.hiddenLayerWeights5[i];
            }

            else
            {
                hiddenLayerWeights5[i] = dad.hiddenLayerWeights5[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights5[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights6[i] = mom.hiddenLayerWeights6[i];
            }

            else
            {
                hiddenLayerWeights6[i] = dad.hiddenLayerWeights6[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights6[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights7[i] = mom.hiddenLayerWeights7[i];
            }

            else
            {
                hiddenLayerWeights7[i] = dad.hiddenLayerWeights7[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights7[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                hiddenLayerWeights8[i] = mom.hiddenLayerWeights8[i];
            }

            else
            {
                hiddenLayerWeights8[i] = dad.hiddenLayerWeights8[i];
            }

            if (yesMutate == 1)
            {
                hiddenLayerWeights8[i] += Random.Range(-value, value);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                outputLayerWeights1[i] = mom.outputLayerWeights1[i];
            }

            else
            {
                outputLayerWeights1[i] = dad.outputLayerWeights1[i];
            }

            if (yesMutate == 1)
            {
                outputLayerWeights1[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                outputLayerWeights2[i] = mom.outputLayerWeights2[i];
            }

            else
            {
                outputLayerWeights2[i] = dad.outputLayerWeights2[i];
            }

            if (yesMutate == 1)
            {
                outputLayerWeights2[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                outputLayerWeights3[i] = mom.outputLayerWeights3[i];
            }

            else
            {
                outputLayerWeights3[i] = dad.outputLayerWeights3[i];
            }

            if (yesMutate == 1)
            {
                outputLayerWeights3[i] += Random.Range(-value, value);
            }

            parent = Random.Range(0, 2);
            yesMutate = Random.Range(0, 4);

            if (parent == 0)
            {
                outputLayerWeights4[i] = mom.outputLayerWeights4[i];
            }

            else
            {
                outputLayerWeights4[i] = dad.outputLayerWeights4[i];
            }

            if (yesMutate == 1)
            {
                outputLayerWeights4[i] += Random.Range(-value, value);
            }
        }
    }

    private void Update()
    {
        startdelay -= Time.deltaTime;
        if (startdelay < 0)
        {
            //reset the values in the nodes to 0 every frame
            for (int i = 0; i < 5; i++)
            {
                outputLayerValues[i] = 0;

                if (i < 4)
                {
                    hiddenLayerValues[i] = 0;
                }
            }
            AICarSensors inputs = gameObject.GetComponent<AICarSensors>();

            /// Check the first layer for nodes that fired, then perform additions if they did
            for (int i = 0; i < 4; i++)
            {
                hiddenLayerValues[i] += hiddenLayerWeights1[i] * inputs.ForwardDist();
                hiddenLayerValues[i] += hiddenLayerWeights2[i] * inputs.FrontLeftDist();
                hiddenLayerValues[i] += hiddenLayerWeights3[i] * inputs.FrontRightDist();
                hiddenLayerValues[i] += hiddenLayerWeights4[i] * inputs.FrontDownDist();
                hiddenLayerValues[i] += hiddenLayerWeights5[i] * inputs.LeftDist();
                hiddenLayerValues[i] += hiddenLayerWeights6[i] * inputs.RightDist();
                hiddenLayerValues[i] += hiddenLayerWeights7[i] * inputs.BackLeftDist();
                hiddenLayerValues[i] += hiddenLayerWeights8[i] * inputs.BackRightDist();
            }

            //apply a sigmoid function to the node values in order to lock them between 0 and 1
            //for (int i = 0; i < 8; i++)
            //{
            //    hiddenLayerValues[i] = sigmoid(hiddenLayerValues[i]);
            //}

            ///End of the first layer firing its nodes

            ///Start second layer firing its nodes if they surpass the threshholds

            for (int i = 0; i < 5; i++)
            {
                outputLayerValues[i] += outputLayerWeights1[i] * hiddenLayerValues[0];
                outputLayerValues[i] += outputLayerWeights2[i] * hiddenLayerValues[1];
                outputLayerValues[i] += outputLayerWeights3[i] * hiddenLayerValues[2];
                outputLayerValues[i] += outputLayerWeights4[i] * hiddenLayerValues[3];
            }
            //apply a sigmoid function to the node values in order to lock them between 0 and 1
            //for (int i = 0; i < 8; i++)
            //{
            //    outputLayerValues[i] = sigmoid(outputLayerValues[i]);
            //}
            ///End layer 2 nodes firing

            ///perform the action that the neural net tells the car to perform
            AIDrivingActions controls = GetComponent<AIDrivingActions>();

            float bestvalue = outputLayerValues[0];
            int bestvaluenum = 0;

            for (int i = 1; i < 5; i++)
            {
                if (outputLayerValues[i] > bestvalue)
                {
                    bestvalue = outputLayerValues[i];
                    bestvaluenum = i;
                }
            }

            switch (bestvaluenum)
            {
                case 0:
                    controls.TurnLeft();
                    break;
                case 1:
                    controls.TurnRight();
                    break;
                case 2:
                    controls.Accelerate();
                    break;
                case 3:
                    controls.Decelerate();
                    break;
                case 4:
                    controls.Jump();
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

        newValues.hiddenLayerWeights1.CopyTo(hiddenLayerWeights1, 0);
        newValues.hiddenLayerWeights2.CopyTo(hiddenLayerWeights2, 0);
        newValues.hiddenLayerWeights3.CopyTo(hiddenLayerWeights3, 0);
        newValues.hiddenLayerWeights4.CopyTo(hiddenLayerWeights4, 0);
        newValues.hiddenLayerWeights5.CopyTo(hiddenLayerWeights5, 0);
        newValues.hiddenLayerWeights6.CopyTo(hiddenLayerWeights6, 0);
        newValues.hiddenLayerWeights7.CopyTo(hiddenLayerWeights7, 0);
        newValues.hiddenLayerWeights8.CopyTo(hiddenLayerWeights8, 0);

        // newValues.layer2NodeValues.CopyTo(layer2NodeValues, 0);

        for (int i = 0; i < 4; i++)
        {
            hiddenLayerValues[i] = 0;
        }

        //newValues.layer2Threshholds.CopyTo(layer2Threshholds, 0);

        newValues.outputLayerWeights1.CopyTo(outputLayerWeights1, 0);
        newValues.outputLayerWeights2.CopyTo(outputLayerWeights2, 0);
        newValues.outputLayerWeights3.CopyTo(outputLayerWeights3, 0);
        newValues.outputLayerWeights4.CopyTo(outputLayerWeights4, 0);


        //newValues.layer3NodeValues.CopyTo(layer3NodeValues, 0);
        for (int i = 0; i < 5; i++)
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
