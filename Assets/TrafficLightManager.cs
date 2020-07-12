using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum LightColor{Red, Orange, Green}

public class TrafficLightManager : MonoBehaviour
{
    public List<TrafficLight> trafficLightList = new List<TrafficLight>();
    public List<WalkerLight> walkerLightList = new List<WalkerLight>();


    void Start()
    {
        bool alternate = true;
        foreach(TrafficLight light in trafficLightList) {
            if (alternate) {
                light.SetColor(LightColor.Red);
                alternate = false;
            }
            else {
                light.SetColor(LightColor.Green);
                alternate = true;
            }

        }
        foreach(WalkerLight light in walkerLightList) {
            if (alternate && !light.invert) {
                light.SetColor(LightColor.Green);
                alternate = false;
            }
            else {
                light.SetColor(LightColor.Red);
                alternate = true;
            }
        }
    }
    public void OnMouseUp() {
        foreach(TrafficLight light in trafficLightList) {
            light.MEH();
        }
        foreach(WalkerLight light in walkerLightList) {
            light.SwitchColor();
        }

    }

}
