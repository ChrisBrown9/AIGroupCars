using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] RaceManager raceManager;

    // Update is called once per frame
    void Update()
    {
        transform.position = raceManager.bestCarPosition() + new Vector3(0, 10, 0) - transform.forward * 10;
    }
}
