using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class TrafficLightManager3 : MonoBehaviour
{
    public TrafficLight[] trafficRoadDuo = new TrafficLight[3];
    public WalkerLight[] trafficWalkerDuo = new WalkerLight[3];

    void Start()
    {
        /*
        trafficRoadDuo[0].conjugatedTrafficLight = trafficRoadDuo[3];
        trafficRoadDuo[1].conjugatedTrafficLight = trafficRoadDuo[3];
        trafficRoadDuo[2].conjugatedTrafficLight = trafficRoadDuo[0];
        trafficRoadDuo[0].conjugatedWalkerLight = trafficWalkerDuo[1];
        trafficRoadDuo[1].conjugatedWalkerLight = trafficWalkerDuo[0];

        trafficRoadDuo[0].SetColor(LightColor.Red);
        trafficRoadDuo[1].SetColor(LightColor.Green);

        trafficWalkerDuo[0].conjugatedWalkerLight = trafficWalkerDuo[1];
        trafficWalkerDuo[1].conjugatedWalkerLight = trafficWalkerDuo[0];

        trafficWalkerDuo[0].SetColor(LightColor.Green);
        trafficWalkerDuo[1].SetColor(LightColor.Red);
        */
    }

}
