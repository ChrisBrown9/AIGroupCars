//Sigmoid function gotten from https://stackoverflow.com/questions/412019/math-optimization-in-c-sharp

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesStorage : MonoBehaviour
{
    //values sent to all the layer 2 nodes when each layer1 node fires
    public float[] layer1Node1lines = new float[2];
    public float[] layer1Node2lines = new float[2];
    public float[] layer1Node3lines = new float[2];
    public float[] layer1Node4lines = new float[2];

    //layer 2 current values
    public float[] layer2NodeValues = new float[2];

    //values sent to all layer 3 nodes
    public float[] layer2Node1lines = new float[2];
    public float[] layer2Node2lines = new float[2];
    //public float[] layer2Node3lines = new float[2];
    //public float[] layer2Node4lines = new float[2];

    //trigger node values
    public float[] triggerValues = new float[2];

    float startdelay = 1.5f;

    private void Awake()
    {
        FreshStartValues();
        RandomizeValues(0.5f);
    }
    public void FreshStartValues()
    {
        for (int i = 0; i < 2; i++)
        {
            layer1Node1lines[i] = 0.5f;
            layer1Node2lines[i] = 0.5f;
            layer1Node3lines[i] = 0.5f;
            layer1Node4lines[i] = 0.5f;

            layer2NodeValues[i] = 0;

            layer2Node1lines[i] = 0.5f;
            layer2Node2lines[i] = 0.5f;
            //layer2Node3lines[i] = 0.5f;
            //layer2Node4lines[i] = 0.5f;

            triggerValues[i] = 0;
        }
    }

    public void RandomizeValues(float value)
    {
        for (int i = 0; i < 2; i++)
        {
            //layer1Threshholds[i] += Random.Range(-value, value);

            layer1Node1lines[i] += Random.Range(-value, value);
            layer1Node2lines[i] += Random.Range(-value, value);
            layer1Node3lines[i] += Random.Range(-value, value);
            layer1Node4lines[i] += Random.Range(-value, value);

            layer2NodeValues[i] = 0;

            layer2Node1lines[i] += Random.Range(-value, value);
            layer2Node2lines[i] += Random.Range(-value, value);
            //layer2Node3lines[i] += Random.Range(-value, value);
            //layer2Node4lines[i] += Random.Range(-value, value);

            triggerValues[i] = 0;
        }
    }

    public void makeBaby(ValuesStorage parent)
    {
        int yesMutate;

        ///start layer 1 mutation
        yesMutate = Random.Range(0, 2);

        parent.layer1Node1lines.CopyTo(layer1Node1lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                ///this commented out bit is for if you want choose each line individually
                //yesMutate = Random.Range(0, 2);

                //if (yesMutate == 0)
                //{
                layer1Node1lines[i] += Random.Range(0f, 1f);
             
                //}
            }
        }
        yesMutate = Random.Range(0, 2);

        parent.layer1Node2lines.CopyTo(layer1Node2lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                layer1Node2lines[i] += Random.Range(0f, 1f);
            }
        }
        yesMutate = Random.Range(0, 2);

        parent.layer1Node3lines.CopyTo(layer1Node3lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                layer1Node3lines[i] += Random.Range(0f, 1f);
            }
        }
        yesMutate = Random.Range(0, 2);

        parent.layer1Node4lines.CopyTo(layer1Node4lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                layer1Node4lines[i] += Random.Range(0f, 1f);
            }
        }

        ///End of layer 1 mutation

        ///start layer 2 mutation
        yesMutate = Random.Range(0, 2);

        parent.layer2Node1lines.CopyTo(layer2Node1lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                layer2Node1lines[i] += Random.Range(0f, 1f);
            }
        }
        yesMutate = Random.Range(0, 2);

        parent.layer2Node2lines.CopyTo(layer2Node2lines, 0);

        if (yesMutate == 0)
        {
            for (int i = 0; i < 2; i++)
            {
                layer2Node2lines[i] += Random.Range(0f, 1f);
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
            for (int i = 0; i < 2; i++)
            {
                layer2NodeValues[i] = 0;
                triggerValues[i] = 0;
            }
            AICarSensors inputs = gameObject.GetComponent<AICarSensors>();

            /// Check the first layer for nodes that fired, then perform additions if they did
            for (int i = 0; i < 2; i++)
            {
                layer2NodeValues[i] += layer1Node1lines[i] * inputs.FrontLeftDist();
                layer2NodeValues[i] += layer1Node2lines[i] * inputs.FrontRightDist();
                layer2NodeValues[i] += layer1Node3lines[i] * inputs.FrontLeft2Dist();
                layer2NodeValues[i] += layer1Node4lines[i] * inputs.FrontRight2Dist();
            }

            ///End of the first layer firing its nodes

            ///Start second layer firing its nodes if they surpass the threshholds

            for (int i = 0; i < 2; i++)
            {
                triggerValues[i] += layer2Node1lines[i] * layer2NodeValues[0];
                triggerValues[i] += layer2Node2lines[i] * layer2NodeValues[1];
            }

            ///End layer 2 nodes firing

            ///perform the action that the neural net tells the car to perform
            AIDrivingActions controls = GetComponent<AIDrivingActions>();

            float bestvalue = triggerValues[0];
            int bestvaluenum = 0;

            for (int i = 1; i < 2; i++)
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
            }
            ///End of the AI performing its Job
        }
    }

    //replace all of the current values in the NeuralNet with the newer shinier values that may or may not improve the racing
    //abilities of the car
    public void replaceValues(ValuesStorage newValues)
    {
        newValues.layer1Node1lines.CopyTo(layer1Node1lines, 0);
        newValues.layer1Node2lines.CopyTo(layer1Node2lines, 0);
        newValues.layer1Node3lines.CopyTo(layer1Node3lines, 0);
        newValues.layer1Node4lines.CopyTo(layer1Node4lines, 0);


        newValues.layer2Node1lines.CopyTo(layer2Node1lines, 0);
        newValues.layer2Node2lines.CopyTo(layer2Node2lines, 0);
    }

    public void resetStartDelay()
    {
        startdelay = 1.5f;
    }
}
