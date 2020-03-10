using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [SerializeField] int checkPointnum;
    
    public int getCheckPointNum()
    {
        return checkPointnum;
    }
}
