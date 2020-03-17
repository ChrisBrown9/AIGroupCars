//Group Members: Chris Brown, Aidan Fallis, Zacchary Labas, Sean Binnie

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //gives access through racemanager
    [SerializeField] RaceManager raceManager;

    // Update is called once per frame
    void Update()
    {
        //upadtes to follow best cars position
        transform.position = raceManager.bestCarPosition() + new Vector3(0, 10, 0) - transform.forward * 10;
    }
}
