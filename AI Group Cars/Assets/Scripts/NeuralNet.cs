using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNet : MonoBehaviour
{
    //first layer values needed in order to fire
    public float[] layer1Threshholds = new float[8];

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
    public float[] layer2Threshholds = new float[8];

    //values sent to all layer 3 nodes when the layer 2 values exceed the threshholds
    public float[] layer2Node1lines = new float[8];
    public float[] layer2Node2lines = new float[8];
    public float[] layer2Node3lines = new float[8];
    public float[] layer2Node4lines = new float[8];
    public float[] layer2Node5lines = new float[8];
    public float[] layer2Node6lines = new float[8];
    public float[] layer2Node7lines = new float[8];
    public float[] layer2Node8lines = new float[8];

    //layer 3 current values and threshholds
    public float[] layer3NodeValues = new float[8];
    public float[] layer3Threshholds = new float[8];

    //values sent to all trigger nodes when layer 3 nodes fire
    public float[] layer3Node1lines = new float[5];
    public float[] layer3Node2lines = new float[5];
    public float[] layer3Node3lines = new float[5];
    public float[] layer3Node4lines = new float[5];
    public float[] layer3Node5lines = new float[5];
    public float[] layer3Node6lines = new float[5];
    public float[] layer3Node7lines = new float[5];
    public float[] layer3Node8lines = new float[5];

    //trigger node values and threshholds, if the value exceeds the threshold the action will occur
    public float[] triggerValues = new float[5];
    public float[] triggerThreshholds = new float[5];

    float startdelay = 1.5f;

    public void FreshStartValues()
    {
        for (int i = 0; i < 8; i++)
        {
            layer1Threshholds[i] = 10;

            layer1Node1lines[i] = 10;
            layer1Node2lines[i] = 10;
            layer1Node3lines[i] = 10;
            layer1Node4lines[i] = 10;
            layer1Node5lines[i] = 10;
            layer1Node6lines[i] = 10;
            layer1Node7lines[i] = 10;
            layer1Node8lines[i] = 10;

            layer2NodeValues[i] = 0;
            layer2Threshholds[i] = 10;

            layer2Node1lines[i] = 10;
            layer2Node2lines[i] = 10;
            layer2Node3lines[i] = 10;
            layer2Node4lines[i] = 10;
            layer2Node5lines[i] = 10;
            layer2Node6lines[i] = 10;
            layer2Node7lines[i] = 10;
            layer2Node8lines[i] = 10;

            layer3NodeValues[i] = 0;
            layer3Threshholds[i] = 10;
        }

        for (int i = 0; i < 5; i++)
        {
            layer3Node1lines[i] = 10;
            layer3Node2lines[i] = 10;
            layer3Node3lines[i] = 10;
            layer3Node4lines[i] = 10;
            layer3Node5lines[i] = 10;
            layer3Node6lines[i] = 10;
            layer3Node7lines[i] = 10;
            layer3Node8lines[i] = 10;

            triggerValues[i] = 0;
            triggerThreshholds[i] = 10;
        }
    }

    public void RandomizeValues(float value)
    {
        for (int i = 0; i < 8; i++)
        {
            layer1Threshholds[i] += Random.Range(-value, value);

            layer1Node1lines[i] += Random.Range(-value, value);
            layer1Node2lines[i] += Random.Range(-value, value);
            layer1Node3lines[i] += Random.Range(-value, value);
            layer1Node4lines[i] += Random.Range(-value, value);
            layer1Node5lines[i] += Random.Range(-value, value);
            layer1Node6lines[i] += Random.Range(-value, value);
            layer1Node7lines[i] += Random.Range(-value, value);
            layer1Node8lines[i] += Random.Range(-value, value);

            layer2NodeValues[i] = 0;
            layer2Threshholds[i] += Random.Range(-value, value);

            layer2Node1lines[i] += Random.Range(-value, value);
            layer2Node2lines[i] += Random.Range(-value, value);
            layer2Node3lines[i] += Random.Range(-value, value);
            layer2Node4lines[i] += Random.Range(-value, value);
            layer2Node5lines[i] += Random.Range(-value, value);
            layer2Node6lines[i] += Random.Range(-value, value);
            layer2Node7lines[i] += Random.Range(-value, value);
            layer2Node8lines[i] += Random.Range(-value, value);

            layer3NodeValues[i] = 0;
            layer3Threshholds[i] += Random.Range(-value, value);
        }

        for (int i = 0; i < 5; i++)
        {
            layer3Node1lines[i] += Random.Range(-value, value);
            layer3Node2lines[i] += Random.Range(-value, value);
            layer3Node3lines[i] += Random.Range(-value, value);
            layer3Node4lines[i] += Random.Range(-value, value);
            layer3Node5lines[i] += Random.Range(-value, value);
            layer3Node6lines[i] += Random.Range(-value, value);
            layer3Node7lines[i] += Random.Range(-value, value);
            layer3Node8lines[i] += Random.Range(-value, value);

            triggerValues[i] = 0;
            triggerThreshholds[i] += Random.Range(-value, value);
        }
    }



    private void Update()
    {
        startdelay -= Time.deltaTime;
        if (startdelay < 0)
        {
            //reset the values in the nodes to 0 every frame
            for (int i = 0; i < 8; i++)
            {
                layer2NodeValues[i] = 0;
                layer3NodeValues[i] = 0;
                if (i < 5)
                {
                    triggerValues[i] = 0;
                }
            }


            AICarSensors inputs = gameObject.GetComponent<AICarSensors>();

            /// Check the first layer for nodes that fired, then perform additions if they did
            if (inputs.ForwardDist() > layer1Threshholds[0])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node1lines[i];
                }
            }

            if (inputs.FrontLeftDist() > layer1Threshholds[1])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node2lines[i];
                }
            }

            if (inputs.FrontRightDist() > layer1Threshholds[2])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node3lines[i];
                }
            }

            if (inputs.FrontDownDist() > layer1Threshholds[3])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node4lines[i];
                }
            }

            if (inputs.LeftDist() > layer1Threshholds[4])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node5lines[i];
                }
            }

            if (inputs.RightDist() > layer1Threshholds[5])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node6lines[i];
                }
            }

            if (inputs.BackLeftDist() > layer1Threshholds[6])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node7lines[i];
                }
            }

            if (inputs.BackRightDist() > layer1Threshholds[7])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer2NodeValues[i] += layer1Node8lines[i];
                }
            }
            ///End of the first layer firing its nodes

            ///Start second layer firing its nodes if they surpass the threshholds
            if (layer2NodeValues[0] > layer2Threshholds[0])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node1lines[i];
                }
            }

            if (layer2NodeValues[1] > layer2Threshholds[1])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node2lines[i];
                }
            }

            if (layer2NodeValues[2] > layer2Threshholds[2])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node3lines[i];
                }
            }

            if (layer2NodeValues[3] > layer2Threshholds[3])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node4lines[i];
                }
            }

            if (layer2NodeValues[4] > layer2Threshholds[4])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node5lines[i];
                }
            }

            if (layer2NodeValues[5] > layer2Threshholds[5])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node6lines[i];
                }
            }

            if (layer2NodeValues[6] > layer2Threshholds[6])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node7lines[i];
                }
            }

            if (layer2NodeValues[7] > layer2Threshholds[7])
            {
                for (int i = 0; i < 8; i++)
                {
                    layer3NodeValues[i] += layer2Node8lines[i];
                }
            }
            ///End layer 2 nodes firing

            ///Begin layer 3 nodes firing if they surpass threshholds
            if (layer3NodeValues[0] > layer3Threshholds[0])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node1lines[i];
                }
            }

            if (layer3NodeValues[1] > layer3Threshholds[1])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node2lines[i];
                }
            }

            if (layer3NodeValues[2] > layer3Threshholds[2])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node3lines[i];
                }
            }

            if (layer3NodeValues[3] > layer3Threshholds[3])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node4lines[i];
                }
            }

            if (layer3NodeValues[4] > layer3Threshholds[4])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node5lines[i];
                }
            }

            if (layer3NodeValues[5] > layer3Threshholds[5])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node6lines[i];
                }
            }

            if (layer3NodeValues[6] > layer3Threshholds[6])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node7lines[i];
                }
            }

            if (layer3NodeValues[7] > layer3Threshholds[7])
            {
                for (int i = 0; i < 5; i++)
                {
                    triggerValues[i] += layer3Node8lines[i];
                }
            }
            ///End layer 3 nodes firing


            ///perform the action that the neural net tells the car to perform
            AIDrivingActions controls = GetComponent<AIDrivingActions>();

            if (triggerValues[0] > triggerThreshholds[0])
            {
                controls.TurnLeft();
            }

            if (triggerValues[1] > triggerThreshholds[1])
            {
                controls.TurnRight();
            }

            if (triggerValues[2] > triggerThreshholds[2])
            {
                controls.Accelerate();
            }

            if (triggerValues[3] > triggerThreshholds[3])
            {
                controls.Decelerate();
            }

            if (triggerValues[4] > triggerThreshholds[4])
            {
                controls.Jump();
            }
            ///End of the AI performing its Job
        }
    }

    //replace all of the current values in the NeuralNet with the newer shinier values that may or may not improve the racing
    //abilities of the car
    public void replaceValues(NeuralNet newValues)
    {
        newValues.layer1Threshholds.CopyTo(layer1Threshholds, 0);

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

        newValues.layer2Threshholds.CopyTo(layer2Threshholds, 0);

        newValues.layer2Node1lines.CopyTo(layer2Node1lines, 0);
        newValues.layer2Node2lines.CopyTo(layer2Node2lines, 0);
        newValues.layer2Node3lines.CopyTo(layer2Node3lines, 0);
        newValues.layer2Node4lines.CopyTo(layer2Node4lines, 0);
        newValues.layer2Node5lines.CopyTo(layer2Node5lines, 0);
        newValues.layer2Node6lines.CopyTo(layer2Node6lines, 0);
        newValues.layer2Node7lines.CopyTo(layer2Node7lines, 0);
        newValues.layer2Node8lines.CopyTo(layer2Node8lines, 0);

        //newValues.layer3NodeValues.CopyTo(layer3NodeValues, 0);
        for (int i = 0; i < 8; i++)
        {
            layer3NodeValues[i] = 0;
        }

        newValues.layer3Threshholds.CopyTo(layer3Threshholds, 0);

        newValues.layer3Node1lines.CopyTo(layer3Node1lines, 0);
        newValues.layer3Node2lines.CopyTo(layer3Node2lines, 0);
        newValues.layer3Node3lines.CopyTo(layer3Node3lines, 0);
        newValues.layer3Node4lines.CopyTo(layer3Node4lines, 0);
        newValues.layer3Node5lines.CopyTo(layer3Node5lines, 0);
        newValues.layer3Node6lines.CopyTo(layer3Node6lines, 0);
        newValues.layer3Node7lines.CopyTo(layer3Node7lines, 0);
        newValues.layer3Node8lines.CopyTo(layer3Node8lines, 0);

        //newValues.triggerValues.CopyTo(triggerValues, 0);
        for (int i = 0; i < 5; i++)
        {
            triggerValues[i] = 0;
        }

        newValues.triggerThreshholds.CopyTo(triggerThreshholds, 0);
    }

    public void resetStartDelay()
    {
        startdelay = 1.5f;
    }
}
