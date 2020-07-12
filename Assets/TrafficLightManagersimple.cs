using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class TrafficLightManagersimple : MonoBehaviour
{
    public TrafficLightsimple[] trafficRoadDuo = new TrafficLightsimple[2];
    public WalkerLight[] trafficWalkerDuo = new WalkerLight[2];
    

    void Start()
    {
        

        trafficRoadDuo[0].SetColor(LightColor.Green);
        trafficRoadDuo[1].SetColor(LightColor.Green);


        trafficWalkerDuo[0].SetColor(LightColor.Red);
        trafficWalkerDuo[1].SetColor(LightColor.Red);
    }

}
