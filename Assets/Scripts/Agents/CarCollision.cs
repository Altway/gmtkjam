using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    public GameObject fearZone;
    public Car myCar;
    void Start()
    {
        myCar = gameObject.GetComponent<Car>();
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Pedestrian")
        {
            col.gameObject.GetComponent<Walker>().state = WalkerState.Death;
            FearZone fz = Instantiate(fearZone, transform.position, transform.rotation).GetComponent<FearZone>();
            fz.emittingNode = myCar.currentNode;
        }
        if(col.tag == "Car")
        {
            col.gameObject.GetComponent<Car>().state = CarState.Broken;
            FearZone fz = Instantiate(fearZone, transform.position, transform.rotation).GetComponent<FearZone>();
            fz.emittingNode = myCar.currentNode;
            gameObject.GetComponent<Car>().state = CarState.Broken;
        }
    }
}
