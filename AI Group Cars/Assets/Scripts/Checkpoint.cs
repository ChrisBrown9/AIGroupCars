using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //stores value of current checkpoint so cars know which checkpoint they've passed 
    [SerializeField] int checkPointnum;
    
    //returns whatever checkpoint they're on
    public int getCheckPointNum()
    {
        return checkPointnum;
    }
}
