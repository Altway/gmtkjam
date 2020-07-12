using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class TrafficLightManager4 : MonoBehaviour
{
    public TrafficLight[] trafficRoadDuo = new TrafficLight[4];
    public WalkerLight[] trafficWalkerDuo = new WalkerLight[4];

    void Start()
    {
        trafficRoadDuo[0].conjugatedTrafficLight = trafficRoadDuo[1];
        trafficRoadDuo[1].conjugatedTrafficLight = trafficRoadDuo[2];
        trafficRoadDuo[2].conjugatedTrafficLight = trafficRoadDuo[3];
        trafficRoadDuo[3].conjugatedTrafficLight = trafficRoadDuo[0];
        trafficRoadDuo[0].conjugatedWalkerLight = trafficWalkerDuo[1];
        trafficRoadDuo[1].conjugatedWalkerLight = trafficWalkerDuo[2];
        trafficRoadDuo[2].conjugatedWalkerLight = trafficWalkerDuo[3];
        trafficRoadDuo[3].conjugatedWalkerLight = trafficWalkerDuo[0];

        trafficRoadDuo[0].SetColor(LightColor.Red);
        trafficRoadDuo[1].SetColor(LightColor.Green);
        trafficRoadDuo[2].SetColor(LightColor.Red);
        trafficRoadDuo[3].SetColor(LightColor.Green);

        trafficWalkerDuo[0].SetColor(LightColor.Green);
        trafficWalkerDuo[1].SetColor(LightColor.Red);
        trafficWalkerDuo[2].SetColor(LightColor.Green);
        trafficWalkerDuo[3].SetColor(LightColor.Red);
    }

}
