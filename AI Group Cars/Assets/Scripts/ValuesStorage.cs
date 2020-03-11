﻿//Sigmoid function gotten from https://stackoverflow.com/questions/412019/math-optimization-in-c-sharp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesStorage : MonoBehaviour
{
    //first layer values needed in order to fire
    //public float[] layer1Threshholds = new float[8];

    //values sent to all the layer 2 nodes when each layer1 node fires
    public float[] layer1Node1lines = new float[8];
    public float[] layer1Node2lines = new float[8];
    public float[] layer1Node3lines = new float[8];
    public float[] layer1Node4lines = new float[8];
    public float[] layer1Node5lines = new float[8];
    public float[] layer1Node6lines = new float[8];
    public float[] layer1Node7lines = new float[8];
    public float[] layer1Node8lines = new float[8];

    //layer 2 current values and threshholds
    public float[] layer2NodeValues = new float[8];
    //public float[] layer2Threshholds = new float[8];

    //values sent to all layer 3 nodes when the layer 2 values exceed the threshholds
    public float[] layer2Node1lines = new float[5];
    public float[] layer2Node2lines = new float[5];
    public float[] layer2Node3lines = new float[5];
    public float[] layer2Node4lines = new float[5];
    public float[] layer2Node5lines = new float[5];
    public float[] layer2Node6lines = new float[5];
    public float[] layer2Node7lines = new float[5];
    public float[] layer2Node8lines = new float[5];


    //trigger node values and threshholds, if the value exceeds the threshold the action will occur
    public float[] triggerValues = new float[5];

    float startdelay = 1.5f;

    private void Awake()
    {
        //FreshStartValues();
        //RandomizeValues(0.5f);
    }
    public void FreshStartValues()
    {
        for (int i = 0; i < 8; i++)
        {
            layer1Node1lines[i] = 0.5f;
            layer1Node2lines[i] = 0.5f;
            layer1Node3lines[i] = 0.5f;
            layer1Node4lines[i] = 0.5f;
            layer1Node5lines[i] = 0.5f;
            layer1Node6lines[i] = 0.5f;
            layer1Node7lines[i] = 0.5f;
            layer1Node8lines[i] = 0.5f;

            layer2NodeValues[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            layer2Node1lines[i] = 0.5f;
            layer2Node2lines[i] = 0.5f;
            layer2Node3lines[i] = 0.5f;
            layer2Node4lines[i] = 0.5f;
            layer2Node5lines[i] = 0.5f;
            layer2Node6lines[i] = 0.5f;
            layer2Node7lines[i] = 0.5f;
            layer2Node8lines[i] = 0.5f;

            triggerValues[i] = 0;
        }
    }

    public void RandomizeValues(float value)
    {
        for (int i = 0; i < 8; i++)
        {
            //layer1Threshholds[i] += Random.Range(-value, value);

            layer1Node1lines[i] += Random.Range(-value, value);
            layer1Node2lines[i] += Random.Range(-value, value);
            layer1Node3lines[i] += Random.Range(-value, value);
            layer1Node4lines[i] += Random.Range(-value, value);
            layer1Node5lines[i] += Random.Range(-value, value);
            layer1Node6lines[i] += Random.Range(-value, value);
            layer1Node7lines[i] += Random.Range(-value, value);
            layer1Node8lines[i] += Random.Range(-value, value);

            layer2NodeValues[i] = 0;
        }

        for (int i = 0; i < 5; i++)
        {
            layer2Node1lines[i] += Random.Range(-value, value);
            layer2Node2lines[i] += Random.Range(-value, value);
            layer2Node3lines[i] += Random.Range(-value, value);
            layer2Node4lines[i] += Random.Range(-value, value);
            layer2Node5lines[i] += Random.Range(-value, value);
            layer2Node6lines[i] += Random.Range(-value, value);
            layer2Node7lines[i] += Random.Range(-value, value);
            layer2Node8lines[i] += Random.Range(-value, value);

            triggerValues[i] = 0;
        }
    }

    public void makeBaby(ValuesStorage parent, float randomvalue)
    {
        int yesMutate;

        ///start layer 1 mutation
        yesMutate = Random.Range(0, 3);

        parent.layer1Node1lines.CopyTo(layer1Node1lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node1lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node2lines.CopyTo(layer1Node2lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //    yesMutate = Random.Range(0, 2);

                //    if (yesMutate == 0)
                //    {
                layer1Node2lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node3lines.CopyTo(layer1Node3lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node3lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node4lines.CopyTo(layer1Node4lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node4lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node5lines.CopyTo(layer1Node5lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node5lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node6lines.CopyTo(layer1Node6lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node6lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node7lines.CopyTo(layer1Node7lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node7lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer1Node8lines.CopyTo(layer1Node8lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node8lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer1Node1lines[i]);
                //}
            }
        }
        ///End of layer 1 mutation

        ///start layer 2 mutation
        yesMutate = Random.Range(0, 3);

        parent.layer2Node1lines.CopyTo(layer2Node1lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node1lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node2lines.CopyTo(layer2Node2lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node2lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node3lines.CopyTo(layer2Node3lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);
                //
                //if (yesMutate == 0)
                //{
                layer2Node3lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node4lines.CopyTo(layer2Node4lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node4lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node5lines.CopyTo(layer2Node5lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node5lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node6lines.CopyTo(layer2Node6lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node6lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node7lines.CopyTo(layer2Node7lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node7lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        yesMutate = Random.Range(0, 3);

        parent.layer2Node8lines.CopyTo(layer2Node8lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer2Node8lines[i] += Random.Range(-randomvalue, randomvalue);
                sigmoid(layer2Node1lines[i]);
                //}
            }
        }
        ///End of layer 2 mutation
    }

    private void FixedUpdate()
    {
        startdelay -= Time.deltaTime;
        if (startdelay < 0)
        {
            //reset the values in the nodes to 0 every frame
            for (int i = 0; i < 8; i++)
            {
                layer2NodeValues[i] = 0;
                if (i < 5)
                {
                    triggerValues[i] = 0;
                }
            }
            AICarSensors inputs = gameObject.GetComponent<AICarSensors>();

            /// Check the first layer for nodes that fired, then perform additions if they did
            for (int i = 0; i < 8; i++)
            {
                layer2NodeValues[i] += layer1Node1lines[i] * inputs.ForwardDist();
                layer2NodeValues[i] += layer1Node2lines[i] * inputs.FrontLeftDist();
                layer2NodeValues[i] += layer1Node3lines[i] * inputs.FrontRightDist();
                layer2NodeValues[i] += layer1Node4lines[i] * inputs.FrontDownDist();
                layer2NodeValues[i] += layer1Node5lines[i] * inputs.LeftDist();
                layer2NodeValues[i] += layer1Node6lines[i] * inputs.RightDist();
                layer2NodeValues[i] += layer1Node7lines[i] * inputs.BackLeftDist();
                layer2NodeValues[i] += layer1Node8lines[i] * inputs.BackRightDist();
            }

            //apply a sigmoid function to the node values in order to lock them between 0 and 1
            for (int i = 0; i < 8; i++)
            {
                layer2NodeValues[i] = sigmoid(layer2NodeValues[i]);
            }

            ///End of the first layer firing its nodes

            ///Start second layer firing its nodes if they surpass the threshholds

            for (int i = 0; i < 5; i++)
            {
                triggerValues[i] += layer2Node1lines[i] * layer2NodeValues[0];
                triggerValues[i] += layer2Node2lines[i] * layer2NodeValues[1];
                triggerValues[i] += layer2Node3lines[i] * layer2NodeValues[2];
                triggerValues[i] += layer2Node4lines[i] * layer2NodeValues[3];
                triggerValues[i] += layer2Node5lines[i] * layer2NodeValues[4];
                triggerValues[i] += layer2Node6lines[i] * layer2NodeValues[5];
                triggerValues[i] += layer2Node7lines[i] * layer2NodeValues[6];
                triggerValues[i] += layer2Node8lines[i] * layer2NodeValues[7];
            }
            //apply a sigmoid function to the node values in order to lock them between 0 and 1
            for (int i = 0; i < 5; i++)
            {
                triggerValues[i] = sigmoid(triggerValues[i]);
            }
            ///End layer 2 nodes firing


            ///perform the action that the neural net tells the car to perform
            AIDrivingActions controls = GetComponent<AIDrivingActions>();

            float bestvalue = triggerValues[0];
            int bestvaluenum = 0;

            for (int i = 1; i < 5; i++)
            {
                if (triggerValues[i] > bestvalue)
                {
                    bestvalue = triggerValues[i];
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

        newValues.layer1Node1lines.CopyTo(layer1Node1lines, 0);
        newValues.layer1Node2lines.CopyTo(layer1Node2lines, 0);
        newValues.layer1Node3lines.CopyTo(layer1Node3lines, 0);
        newValues.layer1Node4lines.CopyTo(layer1Node4lines, 0);
        newValues.layer1Node5lines.CopyTo(layer1Node5lines, 0);
        newValues.layer1Node6lines.CopyTo(layer1Node6lines, 0);
        newValues.layer1Node7lines.CopyTo(layer1Node7lines, 0);
        newValues.layer1Node8lines.CopyTo(layer1Node8lines, 0);

        // newValues.layer2NodeValues.CopyTo(layer2NodeValues, 0);

        for (int i = 0; i < 8; i++)
        {
            layer2NodeValues[i] = 0;
        }

        //newValues.layer2Threshholds.CopyTo(layer2Threshholds, 0);

        newValues.layer2Node1lines.CopyTo(layer2Node1lines, 0);
        newValues.layer2Node2lines.CopyTo(layer2Node2lines, 0);
        newValues.layer2Node3lines.CopyTo(layer2Node3lines, 0);
        newValues.layer2Node4lines.CopyTo(layer2Node4lines, 0);
        newValues.layer2Node5lines.CopyTo(layer2Node5lines, 0);
        newValues.layer2Node6lines.CopyTo(layer2Node6lines, 0);
        newValues.layer2Node7lines.CopyTo(layer2Node7lines, 0);
        newValues.layer2Node8lines.CopyTo(layer2Node8lines, 0);

        //newValues.triggerValues.CopyTo(triggerValues, 0);
        for (int i = 0; i < 5; i++)
        {
            triggerValues[i] = 0;
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
